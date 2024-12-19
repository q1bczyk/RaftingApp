import { Component } from '@angular/core';
import { ReservationFormCardComponent } from "../reservation-form-card/reservation-form-card.component";

@Component({
  selector: 'app-payment-method',
  standalone: true,
  imports: [ReservationFormCardComponent],
  templateUrl: './payment-method.component.html',
  styleUrl: './payment-method.component.scss'
})
export class PaymentMethodComponent {
  paymentMethod: string = 'online';
}
