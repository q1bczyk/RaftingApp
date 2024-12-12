import { Component } from '@angular/core';
import { FormComponent } from "../../../../../shared/ui/form/form.component";
import { FormSettingType } from '../../../../../shared/types/ui/form.type';
import { contactSettingsForm } from '../../../../forms/settings-forms/contact-data-form';
import { SystemSettingsHandlerService } from '../../../../../shared/services/others/system-settings-handler.service';
import { GetSystemSettingsType } from '../../../../../shared/types/api/system-settings-types/get-system-settings.type';
import { FormGroup } from '@angular/forms';
import { mapFormToModel } from '../../../../../core/utils/mapper/mapper';

@Component({
  selector: 'app-contact-settings',
  standalone: true,
  imports: [FormComponent],
  templateUrl: './contact-settings.component.html',
  styleUrl: './contact-settings.component.scss'
})
export class ContactSettingsComponent {
  form : FormSettingType = contactSettingsForm;

  constructor(private systemSettingsService : SystemSettingsHandlerService){
    const settings : GetSystemSettingsType | undefined = systemSettingsService.getSystemSettings();
    if(settings){
      this.form.formGroup.patchValue({
        email: settings.email,
        phoneNumber : settings.phoneNumber
      });
    } 
  }

  onFormSubmit(formData : FormGroup) : void{
    const dataToSend : {email : string, phoneNumber : string} = mapFormToModel(formData);
    this.systemSettingsService.updateContact(dataToSend.email, Number(dataToSend.phoneNumber));
  }

}
