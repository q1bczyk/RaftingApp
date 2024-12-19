import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, map } from "rxjs";
import { CrudService } from "../../../core/services/crud.service";
import { MakeReservationType } from "../../types/api/reservation-types/make-reservation.type";
import { IReservationService } from "../../interfaces/reservation-service.interface";

@Injectable({
    providedIn: 'root'
})

export class ReservationService extends CrudService<any, any, any> implements IReservationService
{
    constructor(protected override http : HttpClient){
        super(http, 'reservation');
    }

    makeReservation(reservationDetails: MakeReservationType): Observable<any> {
        return this.http.post<any>(`${this.url}`, reservationDetails)
            .pipe(
                map(res => {
                    return res;
                })
            )
    }
}