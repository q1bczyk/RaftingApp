import { JsonPipe } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { NgbDatepickerModule, NgbTimepickerModule, NgbTimeStruct } from '@ng-bootstrap/ng-bootstrap';
import { SystemSettingsHandlerService } from '../../../../../shared/services/others/system-settings-handler.service';
import { convertTime } from '../../helpers/dateConventer';
import { GetSystemSettingsType } from '../../../../../shared/types/api/system-settings-types/get-system-settings.type';

@Component({
  selector: 'app-date-settings',
  standalone: true,
  imports: [NgbTimepickerModule, JsonPipe, FormsModule, NgbDatepickerModule],
  templateUrl: './date-settings.component.html',
  styleUrl: './date-settings.component.scss'
})
export class DateSettingsComponent {
  systemSettings : GetSystemSettingsType | undefined;
  openTime: NgbTimeStruct = { hour: 13, minute: 30, second: 0 };
  closeTime: NgbTimeStruct = { hour: 13, minute: 30, second: 0 };
  
  constructor(systemSettingsService : SystemSettingsHandlerService){
      this.systemSettings = systemSettingsService.getSystemSettings();
      if(this.systemSettings){
        this.openTime = convertTime(new Date(this.systemSettings.openingTime));
        this.closeTime = convertTime(new Date(this.systemSettings.closeTime));
      }
  }

}
