import { BaseResolver } from "../../core/resolver/base.resolver";
import { BaseReservationType } from "../../shared/types/api/reservation-types/base-reservation.type";
import { ReservationService } from "../../shared/services/api/reservation.service";
import { Injectable } from "@angular/core";
import { Router } from "@angular/router";

@Injectable({
    providedIn: 'root'
})
export class ReservationResolver extends BaseResolver<BaseReservationType, ReservationService>{
    constructor(service : ReservationService, router : Router){
        super(service, router);
    }
}