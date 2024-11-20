import { Injectable, WritableSignal, signal } from "@angular/core";
import { Observable, take } from "rxjs";
import { LoadingService } from "../../shared/services/loading.service";

@Injectable({
    providedIn: 'root' 
})

export class ApiManager<GetType>{
    data : WritableSignal<GetType | undefined> = signal(undefined);
    protected action$? : Observable<GetType> | Observable<GetType[]> 

    constructor(public loadingService : LoadingService){}

    exeApiRequest(action$ : Observable<GetType>, onSuccess? : () => void)
    {
        this.loadingService.loadingOn();
        this.action$ = action$;
        this.action$
        .pipe(
            take(1)
        )
        .subscribe((data : GetType | undefined) => {
            this.data.set(data);
            onSuccess ? onSuccess() : null;
            this.loadingService.loadingOff();
        });
    }
}