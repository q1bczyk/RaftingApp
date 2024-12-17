import { Injectable, WritableSignal, signal } from "@angular/core";
import { GetEquipmentType } from "../../../../shared/types/api/equipment-types/get-equipment.type";
import { MakeReservationType, ReservationEquipmentType } from "../../../../shared/types/api/reservation-types/make-reservation.type";
import { reservationInitialState } from "./reservation-initial-state";

@Injectable({
    providedIn: 'root',
})
export class ReservationStateService {
    private currentStep: WritableSignal<number> = signal(1);
    private avaiableEquipment: WritableSignal<GetEquipmentType[]> = signal([]);
    private reservationData: WritableSignal<MakeReservationType> = signal(reservationInitialState);
    private isMenuOpen: WritableSignal<boolean> = signal(false);
    private participantsLeft : WritableSignal<number> = signal(0);

    getCurrentStep(): number {
        return this.currentStep();
    }

    getAvaiableEquipment(): GetEquipmentType[] {
        return this.avaiableEquipment();
    }

    getParticipants(): {participantsNumber : number, participantsLeft : number} {
        return {participantsNumber: this.reservationData().participantsNumber, participantsLeft : this.participantsLeft()}
    }

    submitFirstStep(participants: number, equipment: GetEquipmentType[], date: Date) {
        this.currentStep.set(2);
        this.avaiableEquipment.set(equipment);
        this.reservationData.set({
            ...this.reservationData(),
            participantsNumber: participants,
            executionDate: date,
        })
        this.participantsLeft.set(participants);
    }

    selectEquipment(equipment: ReservationEquipmentType): void {
        const updatedReservationEquipment: ReservationEquipmentType[] = [...this.reservationData().reservationEquipment, equipment];
        this.reservationData.set({
            ...this.reservationData(),
            reservationEquipment: updatedReservationEquipment,
        });
        const avaiableEquipmentIndex = this.avaiableEquipment().findIndex(eq => eq.id === equipment.equipmentTypeId)
        this.avaiableEquipment()[avaiableEquipmentIndex].quantity -= 1;

    }

    uncheckEquipment(paraticipants: number, eqId: string, eqIndex: number): void {
        const avaiableEquipmentIndex = this.avaiableEquipment().findIndex(eq => eq.id === eqId)
        this.avaiableEquipment()[avaiableEquipmentIndex].quantity += 1;

        const updatedReservationEquipment = this.reservationData().reservationEquipment.filter((_, index) => index !== eqIndex);
        this.reservationData.set({
            ...this.reservationData(),
            reservationEquipment: updatedReservationEquipment,
        });

    }

    getSelectedEquipment(): ReservationEquipmentType[] {
        return this.reservationData().reservationEquipment;
    }

    setMenuState(isMenuOpen: boolean) {
        this.isMenuOpen.set(isMenuOpen);
    }

    menuState(): boolean {
        return this.isMenuOpen();
    }

    getSelectedEqName(eqId: string): string {
        const selectedEquipment: GetEquipmentType | undefined = this.avaiableEquipment().find(eq => eq.id === eqId);
        return selectedEquipment?.typeName || '';
    }

    setParticipantLeft(value : number) : void{
        const updatedParticipants : number = this.participantsLeft() - value;
        this.participantsLeft.set(updatedParticipants); 
    }

}