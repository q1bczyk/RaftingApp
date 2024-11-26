import { Component, OnInit } from '@angular/core';
import { AuthFormComponent } from '../../components/auth-form/auth-form.component';
import { HttpClient } from '@angular/common/http';
import { LoadingService } from '../../../shared/services/loading.service';
import { ApiSuccessResponse } from '../../../core/types/api-success-response.type';
import { ApiManager } from '../../../core/api/api-manager';
import { AuthService } from '../../services/auth.service';
import { newPasswordForm } from '../../forms/new-password-form';
import { BaseAuthComponent } from '../../directives/base-auth.component';
import { NewPasswordType } from '../../types/new-password.type';
import { FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-new-password',
  standalone: true,
  imports: [AuthFormComponent],
  templateUrl: './new-password.component.html',
  styleUrl: './new-password.component.scss'
})
export class NewPasswordComponent extends BaseAuthComponent<ApiSuccessResponse, NewPasswordType> implements OnInit{

  private token : string = '';
  private userId : string = '';
  constructor(
    private http: HttpClient,
    private loadingService: LoadingService,
    private route : ActivatedRoute,
    private router : Router,
  ){
    super(new AuthService(http), new ApiManager<ApiSuccessResponse>(loadingService), newPasswordForm);
  }

  ngOnInit(): void {
    const token = this.route.snapshot.queryParamMap.get('token');
    const userId = this.route.snapshot.queryParamMap.get('userId');

    if(typeof token === 'string' && typeof userId === 'string'){
      this.token = this.decodeToken(token),
      this.userId = userId
    }
    else
      this.router.navigate(['not-found']);
  }

  override onFormSubmit(form: FormGroup) : void {
    const mappedForm : {password : string, confirmPassword : string} = this.convertForm(form)
    const newPasswordData : NewPasswordType = {
      userId : this.userId,
      token : this.token,
      password : mappedForm.password,
      confirmPassword : mappedForm.confirmPassword
    }
    this.apiManager.exeApiRequest(this.authService.setNewPassword(newPasswordData), () => this.onChangePasswordSuccess());
  }

  onChangePasswordSuccess(){
    this.router.navigate(['auth/login']);
  }

  decodeToken(encodedToken: string): string {
    let decodedToken = decodeURIComponent(encodedToken);
    decodedToken = decodeURIComponent(decodedToken);
    decodedToken = decodedToken.replaceAll(' ', '+');
    return decodedToken;
  }

}
