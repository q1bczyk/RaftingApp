import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CardModule } from 'primeng/card';
import { FormComponent } from "../../../shared/ui/form/form.component";
import { FormSettingType } from '../../../shared/types/ui/form.type';
import { loginForm } from '../../forms/login-form';
import { AuthService } from '../../services/auth.service';
import { FormGroup } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { mapFormToModel } from '../../../core/utils/mapper/mapper';
import { LoginType } from '../../types/login.type';
import { ApiManager } from '../../../core/api/api-manager';
import { LoggedInUserType } from '../../types/logged-in-user.type';

@Component({
  selector: 'app-auth-form',
  standalone: true,
  imports: [CardModule, FormComponent, CommonModule],
  templateUrl: './auth-form.component.html',
  styleUrl: './auth-form.component.scss',
})
export class AuthFormComponent 
{
    @Input() form : FormSettingType = { formGroup : new FormGroup({}), fields : {}, buttonLabel : ''};
    @Output() transferFormEvent : EventEmitter<FormGroup> = new EventEmitter<FormGroup>;

    constructor(private authService : AuthService, public apiManager : ApiManager<LoggedInUserType>){}

    onFormSubmit(data : FormGroup){
      this.transferFormEvent.emit(data);
    }
}
