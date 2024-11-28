import { Component } from '@angular/core';
import { AuthFormComponent } from "../../components/auth-form/auth-form.component";
import { LoggedInUserType } from '../../types/logged-in-user.type';
import { LoginType } from '../../types/login.type';
import { BaseAuthComponent } from '../../directives/base-auth.component';
import { FormGroup } from '@angular/forms';
import { loginForm } from '../../forms/login-form';
import { AuthService } from '../../services/auth.service';
import { ApiManager } from '../../../core/api/api-manager';
import { HttpClient } from '@angular/common/http';
import { LoadingService } from '../../../shared/services/loading.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [AuthFormComponent],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent extends BaseAuthComponent<LoggedInUserType, LoginType> {
 
  constructor(
    private http: HttpClient,
    private loadingService: LoadingService,
    private router : Router
  ){
    super(new AuthService(http), new ApiManager<LoggedInUserType>(loadingService), loginForm);
  }

  override onFormSubmit(form: FormGroup) : void {
    const mappedForm : LoginType = this.convertForm(form)
    this.apiManager.exeApiRequest(this.authService.login(mappedForm), () => this.onLoginSuccess());
  }

  passwordResetNavigate(){
    this.router.navigate(["auth/password-reset"]);
  }

  private onLoginSuccess() : void {
    const loggedUser = this.apiManager.data();

    if (loggedUser && loggedUser.token) localStorage.setItem('token', loggedUser.token);
    this.router.navigate(['/admin']);
  }


}
