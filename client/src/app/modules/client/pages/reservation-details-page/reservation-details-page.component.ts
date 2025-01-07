import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DatePipe } from '@angular/common';
import { dateFormat } from '../../../core/date-format/date-format';
import { SingleReservationDetailsType } from '../../../shared/types/api/reservation-types/reservation-details.type';
import { SystemSettingsHandlerService } from '../../../shared/services/others/system-settings-handler.service';
import { GetSystemSettingsType } from '../../../shared/types/api/system-settings-types/get-system-settings.type';

@Component({
  selector: 'app-reservation-details-page',
  standalone: true,
  imports: [DatePipe],
  templateUrl: './reservation-details-page.component.html',
  styleUrl: './reservation-details-page.component.scss'
})
export class ReservationDetailsPageComponent {
  reservationDetails : any;
  dateFormat : string = dateFormat; 
  contact : string = '';
  lastRefundDay : number | undefined = undefined;

  constructor(private route : ActivatedRoute, private settings : SystemSettingsHandlerService){
    const resolvedData = this.route.snapshot.data['reservationDetails'];
    this.reservationDetails = resolvedData
    const settingsBufor : GetSystemSettingsType | undefined = this.settings.getSystemSettings();
    if(settingsBufor){
      this.contact = settingsBufor.phoneNumber.toString();
      this.lastRefundDay = settingsBufor.dayLatestBookingTime;
    }
  }
}
