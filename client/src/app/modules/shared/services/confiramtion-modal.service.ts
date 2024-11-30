import { Injectable, WritableSignal, signal } from "@angular/core";

@Injectable({
    providedIn: 'root',
})
export class ConfirmationModalService{
    private modalState : WritableSignal<boolean> = signal(false);
    private confirmCallback?: () => void;

    openModal(confirmCallback: () => void) : void{
        this.confirmCallback = confirmCallback;
        this.modalState.set(true);
    }

    closeModal() : void{
        this.modalState.set(false);
    }

    confirmModal() : void{
        if(this.confirmCallback) this.confirmCallback();
        this.closeModal();
    }

    isModalOpen() : boolean{
        return this.modalState();
    }
}