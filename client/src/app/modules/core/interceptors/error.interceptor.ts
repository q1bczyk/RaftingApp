import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, catchError } from "rxjs";
import { ApiErrorResponse } from "../types/api-error-response.type";
import { LoadingService } from "../../shared/services/loading.service";
import { Router } from "@angular/router";
import { ToastService } from "../../shared/services/ui/toasts/toast.service";

@Injectable()
export class ErrorInterceptor implements HttpInterceptor{
    constructor(private router : Router, private toastService : ToastService, private loadingService : LoadingService){}

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        
        return next.handle(req).pipe(
            catchError( (e : ApiErrorResponse) => {
                switch(e.error.statusCode){
                    case 500:
                     this.router.navigate(["/server-error"]);
                     break;
                    case 401:
                        this.UnauthorizeHandle();
                        break;
                    case 409:
                        this.toastService.showToast(e.error.details, 'error');
                        break;
                }
                this.loadingService.loadingOff();
                throw e;
            })
        )
    }

    private UnauthorizeHandle() : void{
        const currentPath = this.router.url; 
            if (currentPath === "/auth/login") 
                this.toastService.showToast("Błędny login lub hasło", "error");
            else 
                this.router.navigate(["/auth/login"]);
    }
}

