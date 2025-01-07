import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BaseReservationType } from '../../../shared/types/api/reservation-types/base-reservation.type';

@Component({
  selector: 'app-reservation-details-page',
  standalone: true,
  imports: [],
  templateUrl: './reservation-details-page.component.html',
  styleUrl: './reservation-details-page.component.scss'
})
export class ReservationDetailsPageComponent {
  reservationDetails : BaseReservationType;

  constructor(private route : ActivatedRoute){
    const resolvedData = this.route.snapshot.data['reservationDetails'];
    this.reservationDetails = resolvedData
    console.log(this.reservationDetails);
  }
}
