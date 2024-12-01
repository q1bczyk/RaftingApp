import { Injectable, WritableSignal, signal } from "@angular/core";

@Injectable({
    providedIn: 'root',
})
export class ToastService{
    private toast : WritableSignal<ToastType | undefined> = signal(undefined);

    showToast(message : string, messageType : string) : void{
        this.toast.set({message : message, messageType : messageType});
    }

    closeToast() : void{
        this.toast.set(undefined);
    }

    getToastState() : ToastType | undefined{
        return this.toast();
    }

}

interface ToastType{
    message : string,
    messageType : string,
}