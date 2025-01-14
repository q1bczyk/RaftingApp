import { Component, Input } from '@angular/core';
import { SingleReservationDetailsType } from '../../../../../shared/types/api/reservation-types/reservation-details.type';
import { ReservationItemComponent } from "../../../reservations-page/components/reservation-table/reservation-item/reservation-item.component";

@Component({
  selector: 'app-reservations',
  standalone: true,
  imports: [ReservationItemComponent],
  templateUrl: './reservations.component.html',
  styleUrl: './reservations.component.scss'
})
export class ReservationsComponent {
  @Input() reservations! : SingleReservationDetailsType[];
}
