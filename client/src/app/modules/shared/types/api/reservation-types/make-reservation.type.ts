import { BaseReservationType } from "./base-reservation.type";

export interface MakeReservationType extends BaseReservationType{
    reservationEquipment : ReservationEquipmentType[]
}

export interface ReservationEquipmentType{
    equipmentTypeId : string,
    quantity : number,
    participants : number,
}