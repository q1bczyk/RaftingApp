import { Injectable, WritableSignal, signal } from "@angular/core";
import { GetEquipmentType } from "../../../../shared/types/api/equipment-types/get-equipment.type";
import { MakeReservationType, ReservationEquipmentType } from "../../../../shared/types/api/reservation-types/make-reservation.type";
import { reservationInitialState } from "./reservation-initial-state";
import { BookerType } from "../../../types/ui/booker-type";
import { ConfirmPaymentType } from "../../../../shared/types/api/payment-types/confirm-payment.type";

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
        return {participantsNumber: this.reservationData().participantNumber, participantsLeft : this.participantsLeft()}
    }

    submitFirstStep(participants: number, equipment: GetEquipmentType[], date: Date) {
        this.currentStep.set(2);
        this.avaiableEquipment.set(equipment);
        this.reservationData.set({
            ...this.reservationData(),
            participantNumber: participants,
            executionDate: date,
        })
        this.participantsLeft.set(participants);
    }

    setStep(step : number) : void{
        this.currentStep.set(step);
    }

    selectEquipment(equipment: ReservationEquipmentType, bookingPrice : number): void {
        const updatedReservationEquipment: ReservationEquipmentType[] = [...this.reservationData().reservationEquipment, equipment];
        this.reservationData.set({
            ...this.reservationData(),
            reservationEquipment: updatedReservationEquipment,
            bookPrice : this.reservationData().bookPrice + bookingPrice
        });
        const avaiableEquipmentIndex = this.avaiableEquipment().findIndex(eq => eq.id === equipment.equipmentTypeId)
        this.avaiableEquipment()[avaiableEquipmentIndex].quantity -= 1;

    }

    uncheckEquipment(paraticipants: number, eqId: string, eqIndex: number): void {
        const avaiableEquipmentIndex = this.avaiableEquipment().findIndex(eq => eq.id === eqId);
        this.avaiableEquipment()[avaiableEquipmentIndex].quantity += 1;
        const bookingPrice = this.avaiableEquipment()[avaiableEquipmentIndex].pricePerPerson * this.reservationData().reservationEquipment[eqIndex].participants;

        const updatedReservationEquipment = this.reservationData().reservationEquipment.filter((_, index) => index !== eqIndex);
        this.reservationData.set({
            ...this.reservationData(),
            reservationEquipment: updatedReservationEquipment,
            bookPrice : this.reservationData().bookPrice - bookingPrice
        });

        this.setParticipantLeft(-paraticipants)

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

    toFirstStep() : void{
        this.reservationData.set(reservationInitialState);
        this.currentStep.set(1);
    }

    getBookingPrice() : number{
        return this.reservationData().bookPrice;
    }

    getReservationDetails() : MakeReservationType{
        return this.reservationData();
    }

    makeReservation(bookerData : BookerType, updatedEquipment : ReservationEquipmentType[]){
        this.reservationData.set({
            ...this.reservationData(),
            bookerName: bookerData.bookerName,
            bookerLastname : bookerData.bookerLastname,
            bookerPhoneNumber : Number(bookerData.bookerPhoneNumber),
            bookerEmail : bookerData.bookerEmail,
            reservationEquipment : updatedEquipment
        })
    }

    setPayment(paymentDetails : ConfirmPaymentType){
        this.reservationData.set({
            ...this.reservationData(),
            payment : paymentDetails
        })
    }

}