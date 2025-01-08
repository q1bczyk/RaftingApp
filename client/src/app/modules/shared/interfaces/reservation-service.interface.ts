import { Observable } from "rxjs";
import { ICrudService } from "../../core/interfaces/crud-service.interface";
import { MakeReservationType } from "../types/api/reservation-types/make-reservation.type";
import { SingleReservationDetailsType } from "../types/api/reservation-types/reservation-details.type";

export interface IReservationService extends ICrudService<any, any, any>{
    makeReservation(reservationDetails : MakeReservationType) : Observable<any>
    fetchFilteredReservations(filters : string) : Observable<SingleReservationDetailsType[]>
}