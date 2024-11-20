import { Component } from '@angular/core';
import { CardModule } from 'primeng/card';
import { FormComponent } from "../../../shared/ui/form/form.component";
import { FormSettingType } from '../../../shared/types/ui/form.type';
import { loginForm } from '../../forms/login-form';

@Component({
  selector: 'app-auth-form',
  standalone: true,
  imports: [CardModule, FormComponent],
  templateUrl: './auth-form.component.html',
  styleUrl: './auth-form.component.scss'
})
export class AuthFormComponent {
    loginForm : FormSettingType = loginForm;
}
