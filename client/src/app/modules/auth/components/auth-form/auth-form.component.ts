import { Component } from '@angular/core';
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

    constructor(private authService : AuthService){}

    onFormSubmit(data : FormGroup){
      const mappedForm : LoginType = mapFormToModel<LoginType>(data);
      this.authService.login(mappedForm)
        .pipe(
          take(1),
                catchError((error) => {
                        console.log(error);
                        return of(undefined)
                    })
        )
        .subscribe(res => {
          console.log(res);
        })
        
    }
}
