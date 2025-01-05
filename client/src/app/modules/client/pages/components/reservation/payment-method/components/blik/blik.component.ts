import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { ApiManager } from '../../../../../../../core/api/api-manager';
import { PaymentService } from '../../../../../../../shared/services/api/payment.service';
import { BlikPaymentInitType } from '../../../../../../../shared/types/api/payment-types/blik-payment-init.type';
import { Stripe } from '@stripe/stripe-js';
import { ReservationStateService } from '../../../../../services/states/reservation-state.service';
import { MakeReservationType } from '../../../../../../../shared/types/api/reservation-types/make-reservation.type';
import { BlikConfirmationComponent } from "./blik-confirmation/blik-confirmation.component";

@Component({
  selector: 'app-blik',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormsModule, BlikConfirmationComponent],
  templateUrl: './blik.component.html',
  styleUrl: './blik.component.scss'
})
export class BlikComponent {

  @Input() stripe!: Stripe | null;
  isLoading : boolean = false;
  blikConfirmation : boolean = false;

  blikForm: FormGroup = new FormGroup({
    mail: new FormControl('', [Validators.required, Validators.email]),
    blikCode: new FormControl('', [Validators.required, Validators.minLength(6), Validators.maxLength(6)]),
  })

  constructor(
    private apiManager: ApiManager<{ clientSecret: string }>,
    private paymentService: PaymentService,
    private reservationStateService: ReservationStateService){ 
      const reservationDetails : MakeReservationType = reservationStateService.getReservationDetails();
      this.blikForm.get('mail')?.setValue(reservationDetails.bookerEmail);
    }

  onSubmit(): void {
    this.isLoading = true;
    const paymentDetails: BlikPaymentInitType = {
      amount: this.reservationStateService.getBookingPrice() * 100,
      email: this.blikForm.get('mail')?.value,
      blikCode: this.blikForm.get('blikCode')?.value,
    }
    this.apiManager.exeApiRequest(this.paymentService.blikPaymentInit(paymentDetails), () => this.confirmPayment())
    this.isLoading = false;
  }

  private async confirmPayment(): Promise<void> {
    this.blikConfirmation = true;
    const result = await this.stripe?.confirmBlikPayment(this.apiManager.data()?.clientSecret || '', {
      payment_method: {
        blik: {},
      },
      payment_method_options: {
        blik: {
          code : this.blikForm.get('blikCode')?.value,
        },
      },
      receipt_email : this.blikForm.get('mail')?.value,
    });
    this.blikConfirmation = false;
    console.log(result);
  }
}
