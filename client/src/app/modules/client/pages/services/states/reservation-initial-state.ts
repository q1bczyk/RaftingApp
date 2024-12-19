import { MakeReservationType } from "../../../../shared/types/api/reservation-types/make-reservation.type";

export const reservationInitialState : MakeReservationType = {
    executionDate : new Date(),
    bookerName : '',
    bookerLastname : '',
    bookPrice : 0,
    bookerPhoneNumber : 0,
    bookerEmail : '',
    participantNumber : 0,
    reservationEquipment : []
}