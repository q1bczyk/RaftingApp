import { Component } from '@angular/core';
import { NewPasswordComponent } from '../new-password/new-password.component';
import { AuthService } from '../../services/auth.service';
import { ActivatedRoute, Router } from '@angular/router';
import { LoadingService } from '../../../shared/services/loading.service';
import { AuthFormComponent } from "../../components/auth-form/auth-form.component";
import { FormGroup } from '@angular/forms';
import { NewPasswordType } from '../../types/new-password.type';
import { SetPasswordType } from '../../types/set-password.type';

@Component({
  selector: 'app-confrim-account',
  standalone: true,
  imports: [AuthFormComponent],
  templateUrl: './confrim-account.component.html',
  styleUrl: './confrim-account.component.scss'
})
export class ConfrimAccountComponent extends NewPasswordComponent{
  constructor(
    loadingService: LoadingService,
    route: ActivatedRoute,
    router: Router,
    authService: AuthService
  ) {
    super(loadingService, route, router, authService);
    this.form.buttonLabel = "Potwierdź konto"
  }

  override onFormSubmit(form: FormGroup) : void {
    const mappedForm : {password : string, confirmPassword : string} = this.convertForm(form);
    const passwordData : SetPasswordType = {
      userId : this.userId,
      password : mappedForm.password,
      confirmPassword : mappedForm.confirmPassword
    }
    this.apiManager.exeApiRequest(this.authService.confirmAccount({userId: this.userId, token : this.token}), () => this.onConfirmAccountSuccess(passwordData));
  }

  private onConfirmAccountSuccess(setPasswordData : SetPasswordType) : void{
    this.authService.setPassword(setPasswordData)
      .subscribe(res => {
          this.router.navigate(['auth/login']);
      })
  }
}
