import { GetEquipmentType } from "../equipment-types/get-equipment.type";
import { SingleReservationDetailsType } from "../reservation-types/reservation-details.type";

export interface DashboardDataType{
    reservations : SingleReservationDetailsType[];
    equipment : GetEquipmentType[]
}