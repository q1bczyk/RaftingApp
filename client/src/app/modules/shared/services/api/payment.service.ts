import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { BaseApiService } from "../../../core/services/base-api.service";
import { Observable, map } from "rxjs";
import { BlikPaymentInitType } from "../../types/api/payment-types/blik-payment-init.type";
import { environment } from "../../../../../../env/environment.prod";

@Injectable({
    providedIn: 'root'
})

export class PaymentService extends BaseApiService
{
    private stripePublicKey : string;

    constructor(protected override http : HttpClient){
        super(http, 'payment');
        this.stripePublicKey = environment.stripeKey
    }

    blikPaymentInit(paymentDetails : BlikPaymentInitType): Observable<{clientSecret : string}> {
        return this.http.post<{clientSecret : string}>(`${this.url}/blikPaymentInit`, paymentDetails)
            .pipe(
                map(res => {
                    return res;
                })
            )
    }

}
