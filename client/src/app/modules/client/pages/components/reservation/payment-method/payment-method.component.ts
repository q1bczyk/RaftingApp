import { Component } from '@angular/core';
import { ReservationFormCardComponent } from "../reservation-form-card/reservation-form-card.component";
import { ReservationStateService } from '../../../services/states/reservation-state.service';
import { ReservationService } from '../../../../../shared/services/api/reservation.service';
import { ApiManager } from '../../../../../core/api/api-manager';
import { MakeReservationType } from '../../../../../shared/types/api/reservation-types/make-reservation.type';
import { LoadingService } from '../../../../../shared/services/loading.service';
import { Router } from '@angular/router';

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
    private router : Router,
    ){}

    onSubmit() : void{
      const reservationDetails : MakeReservationType = this.reservationStateService.getReservationDetails();
      console.log(reservationDetails);
      this.apiManager.exeApiRequest(this.service.makeReservation(reservationDetails), () => this.router.navigate(['/reservation/1']))
    }

    isLoading() : boolean{
      return this.apiManager.loadingService.isLoading();
    }

}
