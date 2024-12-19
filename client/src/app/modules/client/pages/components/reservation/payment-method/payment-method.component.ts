import { Component } from '@angular/core';
import { ReservationFormCardComponent } from "../reservation-form-card/reservation-form-card.component";
import { ReservationStateService } from '../../../services/states/reservation-state.service';
import { ReservationService } from '../../../../../shared/services/api/reservation.service';
import { ApiManager } from '../../../../../core/api/api-manager';
import { MakeReservationType } from '../../../../../shared/types/api/reservation-types/make-reservation.type';

@Component({
  selector: 'app-payment-method',
  standalone: true,
  imports: [ReservationFormCardComponent],
  templateUrl: './payment-method.component.html',
  styleUrl: './payment-method.component.scss'
})
export class PaymentMethodComponent {
  paymentMethod: string = 'online';

  constructor(
    private reservationStateService : ReservationStateService, 
    private service : ReservationService,
    private apiManager : ApiManager<any>, 
    ){}

    onSubmit() : void{
      const reservationDetails : MakeReservationType = this.reservationStateService.getReservationDetails();
      console.log(reservationDetails);
      this.apiManager.exeApiRequest(this.service.makeReservation(reservationDetails), () => console.log(this.apiManager.data()))
    }

}
