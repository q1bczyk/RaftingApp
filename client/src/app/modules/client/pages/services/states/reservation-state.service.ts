import { Injectable, WritableSignal, signal } from "@angular/core";

@Injectable({
    providedIn: 'root',
})
export class ReservationStateService{
    private currentStep : WritableSignal<number> = signal(1);

    getCurrentStep() : number{
        return this.currentStep();
    }

    setCurrentStep(step : number) : void{
        this.currentStep.set(step);
    }

}