import { PaymentInitType } from "./payment-init.type";

export interface BlikPaymentInitType extends PaymentInitType{
    blikCode : string;
    email : string;
}