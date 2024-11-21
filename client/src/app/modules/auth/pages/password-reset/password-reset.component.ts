import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { LoadingService } from '../../../shared/services/loading.service';
import { AuthService } from '../../services/auth.service';
import { ApiManager } from '../../../core/api/api-manager';
import { ApiSuccessResponse } from '../../../core/types/api-success-response.type';
import { passwordResetForm } from '../../forms/password-reset-form';
import { BaseAuthComponent } from '../../directives/base-auth.component';
import { BaseAuthType } from '../../types/base-auth.type';
import { FormGroup } from '@angular/forms';
import { AuthFormComponent } from '../../components/auth-form/auth-form.component';

@Component({
  selector: 'app-password-reset',
  standalone: true,
  imports: [AuthFormComponent],
  templateUrl: './password-reset.component.html',
  styleUrl: './password-reset.component.scss'
})
export class PasswordResetComponent extends BaseAuthComponent<ApiSuccessResponse, BaseAuthType>{
  constructor(
    private http: HttpClient,
    private loadingService: LoadingService,
  ){
    super(new AuthService(http), new ApiManager<ApiSuccessResponse>(loadingService), passwordResetForm);
  }

  override onFormSubmit(form: FormGroup) : void {
    const mappedForm : BaseAuthType = this.convertForm(form)
    this.apiManager.exeApiRequest(this.authService.passwordReset(mappedForm));
  }

  private onLoginSuccess() : void{
    
  }
}
