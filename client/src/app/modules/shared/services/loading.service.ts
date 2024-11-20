import { Injectable, WritableSignal, signal } from "@angular/core";

@Injectable({
    providedIn: 'root',
})
export class LoadingService{
    private loading : WritableSignal<boolean> = signal(false);

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