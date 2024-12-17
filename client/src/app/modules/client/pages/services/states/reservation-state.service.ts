import { Injectable, WritableSignal, signal } from "@angular/core";
import { GetEquipmentType } from "../../../../shared/types/api/equipment-types/get-equipment.type";
import { MakeReservationType } from "../../../../shared/types/api/reservation-types/make-reservation.type";
import { reservationInitialState } from "./reservation-initial-state";

@Injectable({
    providedIn: 'root',
})
export class ReservationStateService {
    private currentStep: WritableSignal<number> = signal(1);
    private avaiableEquipment: WritableSignal<GetEquipmentType[]> = signal([]);
    private reservationData : WritableSignal<MakeReservationType> = signal(reservationInitialState);

    getCurrentStep(): number {
        return this.currentStep();
    }

    getAvaiableEquipment(): GetEquipmentType[] {
        return this.avaiableEquipment();
    }

    getParticipants() : number {
        return this.reservationData().participantsNumber;
    }

    submitFirstStep(participants : number, equipment : GetEquipmentType[], date : Date) {
        this.currentStep.set(2);
        this.avaiableEquipment.set(equipment);
        this.reservationData.set({
            ...this.reservationData(),
            participantsNumber: participants,
            executionDate : date,
        })
    }

}