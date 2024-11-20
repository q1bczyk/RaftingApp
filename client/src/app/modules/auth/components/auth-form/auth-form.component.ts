import { Component, Inject } from '@angular/core';
import { CardModule } from 'primeng/card';
import { FormComponent } from "../../../shared/ui/form/form.component";
import { FormSettingType } from '../../../shared/types/ui/form.type';
import { loginForm } from '../../forms/login-form';
import { AuthService } from '../../services/auth.service';
import { FormGroup } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { mapFormToModel } from '../../../core/utils/mapper/mapper';
import { LoginType } from '../../types/login.type';
import { catchError, of, take } from 'rxjs';
import { ApiManager } from '../../../core/api/api-manager';
import { LoggedInUserType } from '../../types/logged-in-user.type';
import { LoadingService } from '../../../shared/services/loading.service';

@Component({
  selector: 'app-auth-form',
  standalone: true,
  imports: [CardModule, FormComponent, CommonModule],
  templateUrl: './auth-form.component.html',
  styleUrl: './auth-form.component.scss',
})
export class AuthFormComponent 
{
    loginForm : FormSettingType = loginForm;

    constructor(private authService : AuthService, public apiManager : ApiManager<LoggedInUserType>){}

    onFormSubmit(data : FormGroup){
      const mappedForm : LoginType = mapFormToModel<LoginType>(data);
      this.apiManager.exeApiRequest(this.authService.login(mappedForm));
    }
}
