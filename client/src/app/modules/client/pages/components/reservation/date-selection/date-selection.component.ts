import { Component } from '@angular/core';
import { FormComponent } from "../../../../../shared/ui/form/form.component";
import { ReservationFormCardComponent } from "../reservation-form-card/reservation-form-card.component";
import { ReservationDetailsType } from '../../../../types/api/reservation-details.type';
import { EquipmentService } from '../../../../../shared/services/api/equipment.service';
import { dateSelectionForm } from '../../../../forms/date-selection-form';
import { FormGroup, FormsModule } from '@angular/forms';
import { mapFormToModel } from '../../../../../core/utils/mapper/mapper';
import { ReservationStateService } from '../../../services/states/reservation-state.service';
import { FormSettingType } from '../../../../../shared/types/ui/form.type';
import { NgbTimeStruct, NgbTimepickerModule } from '@ng-bootstrap/ng-bootstrap';
import { dateSettingsInit } from '../../../../../admin/pages/system-settings-page/components/types/dateSettingsType';
import { convertTime } from '../../../../../admin/pages/system-settings-page/helpers/dateConventer';
import { SystemSettingsHandlerService } from '../../../../../shared/services/others/system-settings-handler.service';
import { GetSystemSettingsType } from '../../../../../shared/types/api/system-settings-types/get-system-settings.type';
import { DatePipe } from '@angular/common';
import { ToastService } from '../../../../../shared/services/ui/toasts/toast.service';

@Component({
  selector: 'app-date-selection',
  standalone: true,
  imports: [FormComponent, ReservationFormCardComponent, NgbTimepickerModule, FormsModule, DatePipe],
  templateUrl: './date-selection.component.html',
  styleUrl: './date-selection.component.scss'
})
export class DateSelectionComponent {
  form: FormSettingType = dateSelectionForm;
  time: NgbTimeStruct = dateSettingsInit().openTime;
  systemSettings : GetSystemSettingsType | undefined = undefined;

  constructor(private reservationStateService: ReservationStateService, private service: EquipmentService, private systemSettingsService : SystemSettingsHandlerService, private toastService : ToastService) {
    const systemSettings = systemSettingsService.getSystemSettings();
    if (systemSettings) {
      this.systemSettings = systemSettings;
      this.time = convertTime(new Date(systemSettings.seasonStartDate));
    }
  }

  timeValid() : void{
    if(!this.systemSettings) return;

    const closeTime = convertTime(new Date(this.systemSettings.seasonEndDate));
    const openTime = convertTime(new Date(this.systemSettings.seasonStartDate));
    let updatedTime = { ...this.time };

    if (updatedTime.hour < openTime.hour) {
      updatedTime.hour = openTime.hour;
      updatedTime.minute = openTime.minute;
    }

    if (updatedTime.hour > closeTime.hour || 
       (updatedTime.hour === closeTime.hour && updatedTime.minute > closeTime.minute)) {
      updatedTime.hour = closeTime.hour;
      updatedTime.minute = closeTime.minute;
    }

    this.time = updatedTime;
  }

  onFormSubmit(form: FormGroup): void {
  const mappedData: ReservationDetailsType = mapFormToModel(form);

  const selectedDate = new Date(mappedData.date);
  selectedDate.setUTCDate(selectedDate.getUTCDate() + 1); 
  selectedDate.setUTCHours(this.time.hour);
  selectedDate.setUTCMinutes(this.time.minute);
  
  mappedData.date = selectedDate; 

  if(!this.isDateValid(mappedData.date)) return;

  this.service.fetchAvaiableEquipment(mappedData)
    .subscribe(res => {
      this.reservationStateService.submitFirstStep(mappedData.participants, res, mappedData.date);
    });
  };

  private isDateValid(date : Date) : boolean{
    if(!this.systemSettings) return false;
    const today = new Date();
    
    if(date < today){
      this.toastService.showToast('Wskazana data jest datą przeszłą', 'error');
      return false;
    }

    const maxDateToBook = new Date(today)
    maxDateToBook.setUTCDate(today.getUTCDate() + this.systemSettings.dayLatestBookingTime);

    if(date <= maxDateToBook){
      this.toastService.showToast('Za późno na rezerwacje w tym termine', 'error');
      return false;
    }

    const minDateToBook = new Date(today)
    minDateToBook.setUTCDate(today.getUTCDate() + this.systemSettings.dayEarliestBookingTime);

    if(date > minDateToBook){
      this.toastService.showToast('Za wcześnie na rezerwacje w tym termine', 'error');
      return false;
    }

    if(date < new Date(this.systemSettings.seasonStartDate) || date > new Date(this.systemSettings.seasonEndDate)){
      this.toastService.showToast('W podanym terminie spływ jest niedostępny', 'error');
      return false;
    }
    
    return true
  } 

}
