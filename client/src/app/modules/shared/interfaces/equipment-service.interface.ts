import { Observable } from "rxjs";
import { ReservationDetailsType } from "../../client/types/api/reservation-details.type";
import { ICrudService } from "../../core/interfaces/crud-service.interface";
import { CreateEquipmentType } from "../types/api/equipment-types/create-equipment.type";
import { GetEquipmentType } from "../types/api/equipment-types/get-equipment.type";
import { UpdateEquipmentType } from "../types/api/equipment-types/update-equipment.type";

export interface IEquipmentService extends ICrudService<GetEquipmentType, CreateEquipmentType, UpdateEquipmentType>{
    fetchAvaiableEquipment(reservationDetails : ReservationDetailsType) : Observable<GetEquipmentType[]>
}