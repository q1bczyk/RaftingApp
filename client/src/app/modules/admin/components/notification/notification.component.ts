import { Component, Input } from '@angular/core';
import { SingleReservationDetailsType } from '../../../shared/types/api/reservation-types/reservation-details.type';
import { DatePipe } from '@angular/common';
import { dateFormat } from '../../../core/date-format/date-format';

@Component({
  selector: 'app-notification',
  standalone: true,
  imports: [DatePipe],
  templateUrl: './notification.component.html',
  styleUrl: './notification.component.scss'
})
export class NotificationComponent {
  dateFormat : string = dateFormat;

  @Input() reservations : SingleReservationDetailsType[] = []
}
