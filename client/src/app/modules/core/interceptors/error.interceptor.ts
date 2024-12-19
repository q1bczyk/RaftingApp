import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { ErrorService } from "../../shared/services/error.service";
import { Observable, catchError } from "rxjs";
import { ApiErrorResponse } from "../types/api-error-response.type";
import { LoadingService } from "../../shared/services/loading.service";
import { Router } from "@angular/router";

@Injectable()
export class ErrorInterceptor implements HttpInterceptor{
    constructor(private router : Router, private errorService : ErrorService, private loadingService : LoadingService){}

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        
        return next.handle(req).pipe(
            catchError( (e : ApiErrorResponse) => {
                console.log(e)
                switch(e.error.statusCode){
                    case 500:
                     this.router.navigate(["/server-error"]);
                     break;
                }
                this.loadingService.loadingOff();
                throw e;
            })
        )
    }

}