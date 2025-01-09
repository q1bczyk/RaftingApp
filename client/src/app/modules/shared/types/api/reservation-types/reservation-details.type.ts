import { GetEquipmentType } from "../equipment-types/get-equipment.type";
import { ConfirmPaymentType } from "../payment-types/confirm-payment.type";
import { BaseReservationType } from "./base-reservation.type";

export interface SingleReservationDetailsType extends BaseReservationType{
    id : string,
    reservationEquipment : ReservationEquipmentType[]
    payment : ConfirmPaymentType
}

export interface ReservationEquipmentType{
    quantity : number,
    participants : number,
    equipmentType : GetEquipmentType;
}