import { Component, EventEmitter, Input, Output } from '@angular/core';
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
  @Output() reservationDeleteEvent : EventEmitter<string> = new EventEmitter<string>
  @Input() reservation! : SingleReservationDetailsType;
  @Input() index! : number;
  dateFormat : string = dateFormat;

  onDelete(id : string){
    this.reservationDeleteEvent.emit(id);
  }
}
