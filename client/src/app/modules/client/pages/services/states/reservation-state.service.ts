import { Injectable, WritableSignal, signal } from "@angular/core";
import { GetEquipmentType } from "../../../../shared/types/api/equipment-types/get-equipment.type";

@Injectable({
    providedIn: 'root',
})
export class ReservationStateService {
    private currentStep: WritableSignal<number> = signal(1);
    private avaiableEquipment: WritableSignal<GetEquipmentType[]> = signal([]);
    private participants: WritableSignal<number> = signal(0);

    getCurrentStep(): number {
        return this.currentStep();
    }

    getAvaiableEquipment(): GetEquipmentType[] {
        return this.avaiableEquipment();
    }

    getParticipants() {
        return this.participants();
    }

    submitFirstStep(participants : number, equipment : GetEquipmentType[]) {
        this.currentStep.set(2);
        this.avaiableEquipment.set(equipment);
        this.participants.set(participants);
    }

}