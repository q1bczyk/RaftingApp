import { Component } from '@angular/core';
import { FormSettingType } from '../../../../../shared/types/ui/form.type';
import { reservationTermsForm } from '../../../../forms/settings-forms/reservation-terms-form';
import { SystemSettingsHandlerService } from '../../../../../shared/services/others/system-settings-handler.service';
import { GetSystemSettingsType } from '../../../../../shared/types/api/system-settings-types/get-system-settings.type';
import { FormGroup } from '@angular/forms';
import { mapFormToModel } from '../../../../../core/utils/mapper/mapper';
import { FormComponent } from "../../../../../shared/ui/form/form.component";

@Component({
  selector: 'app-reservation-terms',
  standalone: true,
  imports: [FormComponent],
  templateUrl: './reservation-terms.component.html',
  styleUrl: './reservation-terms.component.scss'
})
export class ReservationTermsComponent {
  form : FormSettingType = reservationTermsForm;

  constructor(private systemSettingsService : SystemSettingsHandlerService){
    const settings : GetSystemSettingsType | undefined = systemSettingsService.getSystemSettings();
    if(settings){
      this.form.formGroup.patchValue({
        dayEarliestBookingTime: settings.dayEarliestBookingTime,
        dayLatestBookingTime: settings.dayLatestBookingTime,
        hoursRentalTime: settings.hoursRentalTime
      });
    } 
  }

  onFormSubmit(formData : FormGroup) : void{
    const dataToSend : {dayEarliestBookingTime: number, dayLatestBookingTime: number, hoursRentalTime: number} = mapFormToModel(formData);
    this.systemSettingsService.updateReservationTerms(dataToSend.dayEarliestBookingTime, dataToSend.dayLatestBookingTime, dataToSend.hoursRentalTime);
  }
}
