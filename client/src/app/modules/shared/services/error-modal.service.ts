import { Injectable, WritableSignal, signal } from "@angular/core";

@Injectable({
    providedIn: 'root',
})
export class ErrorModalService{
    private errorModal : WritableSignal<ErrorModalType> = signal({isMoadlOpen: false, errorStatusCode : 0, errorMessage : ''});

    loadingOn(){
        this.loading.set(true);
    }

    loadingOff(){
        this.loading.set(false);
    }

    isLoading() : WritableSignal<boolean>{
        return this.loading;
    }
}

interface ErrorModalType{
    isMoadlOpen : boolean,
    errorStatusCode : number,
    errorMessage : string,
}