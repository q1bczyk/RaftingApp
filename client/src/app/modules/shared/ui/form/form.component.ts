import { Component, EventEmitter, Input, Output } from '@angular/core';
import { InputTextModule } from 'primeng/inputtext';
import { ButtonModule } from 'primeng/button';
import { FormSettingType } from '../../types/ui/form.type';
import { FormGroup } from '@angular/forms';
import {ReactiveFormsModule} from '@angular/forms';
import { LoadingService } from '../../services/loading.service';

@Component({
  selector: 'app-form',
  standalone: true,
  imports: [InputTextModule, ButtonModule, ReactiveFormsModule],
  templateUrl: './form.component.html',
  styleUrl: './form.component.scss'
})
export class FormComponent {
  @Input() formSettings : FormSettingType = { formGroup : new FormGroup({}), fields : {}, buttonLabel : ''}
  @Output() formSubmitEvent : EventEmitter<FormGroup> = new EventEmitter<FormGroup>();  

  constructor(public loadingService : LoadingService){}

  get controls() : string[]
  {
    return Object.keys(this.formSettings.formGroup.controls);
  }

  onFormSubmit() : void {
    if(this.formSettings.formGroup.invalid)
      return
    this.loadingService.loadingOn();
    this.formSubmitEvent.emit(this.formSettings.formGroup);
  }

  isFormInvalid(): boolean 
  {
    const controls = this.formSettings.formGroup.controls;
    for (let control in controls) {
      if (controls[control].invalid) {
        return true;
      }
    }
    return false;
  }

}
