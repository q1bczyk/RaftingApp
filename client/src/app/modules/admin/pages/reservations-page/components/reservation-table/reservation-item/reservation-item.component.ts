import { Component, Input } from '@angular/core';
import { SingleReservationDetailsType } from '../../../../../../shared/types/api/reservation-types/reservation-details.type';
import { CommonModule, DatePipe } from '@angular/common';
import { dateFormat } from '../../../../../../core/date-format/date-format';

@Component({
  selector: 'app-reservation-item',
  standalone: true,
  imports: [DatePipe, CommonModule],
  templateUrl: './reservation-item.component.html',
  styleUrl: './reservation-item.component.scss'
})
export class ReservationItemComponent {
  @Input() reservation! : SingleReservationDetailsType;
  @Input() index! : number;
  dateFormat : string = dateFormat;
}
