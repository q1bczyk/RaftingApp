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

  protected token : string = '';
  protected userId : string = '';
  constructor(
    private loadingService: LoadingService,
    private route : ActivatedRoute,
    protected router : Router,
    authService : AuthService
  ){
    super(authService, new ApiManager<ApiSuccessResponse>(loadingService), newPasswordForm);
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
    this.apiManager.exeApiRequest(this.authService.setNewPassword(this.setData(form)), () => this.onChangePasswordSuccess());
  }

  protected onChangePasswordSuccess(){
    this.router.navigate(['auth/login']);
  }

  private decodeToken(encodedToken: string): string {
    let decodedToken = decodeURIComponent(encodedToken);
    decodedToken = decodeURIComponent(decodedToken);
    decodedToken = decodedToken.replaceAll(' ', '+');
    return decodedToken;
  }

  protected setData(form: FormGroup) : NewPasswordType{
    const mappedForm : {password : string, confirmPassword : string} = this.convertForm(form)
    const newPasswordData : NewPasswordType = {
      userId : this.userId,
      token : this.token,
      password : mappedForm.password,
      confirmPassword : mappedForm.confirmPassword
    }
    return newPasswordData;
  }

}
