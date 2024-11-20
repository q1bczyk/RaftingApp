import { Injectable, WritableSignal, signal } from "@angular/core";

@Injectable({
    providedIn: 'root',
})
export class ErrorModalService{
    private errorModal : WritableSignal<ErrorModalType> = signal({isMoadlOpen: true, errorStatusCode : 404, errorMessage : 'Nie znaleziono danego zasobu'});

    get getModal() : ErrorModalType{
        return this.errorModal();
    }

    closeModal(){
        this.errorModal.update(modalState => ({
            ...modalState,
            isMoadlOpen : false
        }));
    }

    openModal(statusCode : number, errorMessage : string){
        this.setError(statusCode, errorMessage)
    }

    private setError(statusCode : number, errorMessage : string){
        const error : ErrorModalType = {
            isMoadlOpen : true,
            errorStatusCode : statusCode,
            errorMessage : errorMessage
        }
        this.errorModal.set(error);
    }
}

interface ErrorModalType{
    isMoadlOpen : boolean,
    errorStatusCode : number,
    errorMessage : string,
}