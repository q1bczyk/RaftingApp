import { ConfirmPaymentType } from "../payment-types/confirm-payment.type";
import { BaseReservationType } from "./base-reservation.type";

export interface MakeReservationType extends BaseReservationType{
    reservationEquipment : ReservationEquipmentType[]
    payment : ConfirmPaymentType | null
}

export interface ReservationEquipmentType{
    equipmentTypeId : string,
    quantity : number,
    participants : number,
}