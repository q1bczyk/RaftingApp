import { JsonPipe } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { NgbDatepickerModule, NgbTimepickerModule} from '@ng-bootstrap/ng-bootstrap';
import { SystemSettingsHandlerService } from '../../../../../shared/services/others/system-settings-handler.service';
import { convertTime, dateToNgbStruct, dateToString, structToDate } from '../../helpers/dateConventer';
import { dateFormat } from '../../../../../core/date-format/date-format';
import { DateSettingsType, dateSettingsInit } from '../types/dateSettingsType';
import { ToastService } from '../../../../../shared/services/ui/toasts/toast.service';

@Component({
  selector: 'app-date-settings',
  standalone: true,
  imports: [NgbTimepickerModule, JsonPipe, FormsModule, NgbDatepickerModule],
  templateUrl: './date-settings.component.html',
  styleUrl: './date-settings.component.scss'
})
export class DateSettingsComponent {
  dateFormat: string = dateFormat;
  dateSettings: DateSettingsType = dateSettingsInit();

  constructor(private systemSettingsService: SystemSettingsHandlerService, private toastService : ToastService) {
    const systemSettings = systemSettingsService.getSystemSettings();
    if (systemSettings) {
      this.dateSettings.openTime = convertTime(new Date(systemSettings.seasonStartDate));
      this.dateSettings.closeTime = convertTime(new Date(systemSettings.seasonEndDate));
      this.dateSettings.openDate = dateToNgbStruct(new Date(systemSettings.seasonStartDate));
      this.dateSettings.closeDate = dateToNgbStruct(new Date(systemSettings.seasonEndDate));
    }
  }

  onUpdate() : void{
    const formatedOpenDate = structToDate(this.dateSettings.openDate, this.dateSettings.openTime);
    const formatedCloseDate = structToDate(this.dateSettings.closeDate, this.dateSettings.closeTime);
    if(!this.dateValid(formatedOpenDate, formatedCloseDate)) return;
    this.systemSettingsService.updateDate(formatedOpenDate, formatedCloseDate);
  }

  dateValid(openDate : Date, closeDate : Date) : boolean{
    const oneDayInMs = 24 * 60 * 60 * 1000; 
    const differenceInDays = (closeDate.getTime() - openDate.getTime()) / oneDayInMs;
    if (differenceInDays >= 1) return true;
    this.toastService.showToast('Różnica między datami musi wynosić minimum 1 dzień', 'error');
    return false;
  }

}
