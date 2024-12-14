import { HttpClient } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";
import { Observable, map } from "rxjs";
import { CrudService } from "../../../core/services/crud.service";
import { GetEquipmentType } from "../../types/api/equipment-types/get-equipment.type";
import { IEquipmentService } from "../../interfaces/equipment-service.interface";
import { ReservationDetailsType } from "../../../client/types/api/reservation-details.type";

@Injectable({
    providedIn: 'root'
})

export class EquipmentService extends CrudService<GetEquipmentType, any, any> implements IEquipmentService
{
    constructor(protected override http : HttpClient){
        super(http, 'equipmentType');
    }

    fetchAvaiableEquipment(reservationDetails: ReservationDetailsType): Observable<GetEquipmentType[]> {
        return this.http.post<GetEquipmentType[]>(`${this.url}/avaiableEquipment`, reservationDetails)
            .pipe(
                map(res => {
                    return res;
                })
            )
    }
}