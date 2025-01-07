import { GetEquipmentType } from "../equipment-types/get-equipment.type";
import { BaseReservationType } from "./base-reservation.type";

export interface SingleReservationDetailsType extends BaseReservationType{
    id : string,
    reservationEquipment : GetEquipmentType[]
}