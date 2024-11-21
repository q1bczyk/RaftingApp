import { Injectable, WritableSignal, signal } from "@angular/core";

@Injectable({
    providedIn: 'root',
})
export class ErrorService{
    private errorHandler : WritableSignal<ErrorHandlerType> = signal({isMoadlOpen: false, errorStatusCode : 404, errorMessage : 'Nie znaleziono danego zasobu'});

    get getError() : ErrorHandlerType{
        return this.errorHandler();
    }

    closeModal(){
        this.errorHandler.update(errorState => ({
            ...errorState,
            isMoadlOpen : false
        }));
    }

    errorWithModal(statusCode : number, errorMessage : string){
        this.setError(statusCode, errorMessage, true)
    }

    public setError(statusCode : number, errorMessage : string, openModal : boolean){
        const error : ErrorHandlerType = {
            isMoadlOpen : openModal,
            errorStatusCode : statusCode,
            errorMessage : errorMessage
        }
        this.errorHandler.set(error);
    }
}

interface ErrorHandlerType{
    isMoadlOpen : boolean,
    errorStatusCode : number,
    errorMessage : string,
}