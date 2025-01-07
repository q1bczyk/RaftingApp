import { Inject, Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, MaybeAsync, Resolve, Router, RouterStateSnapshot } from "@angular/router";
import { catchError, map, of } from "rxjs";
import { CrudService } from "../services/crud.service";

@Injectable({
    providedIn: 'root'
})
export class BaseResolver<TResponse, TService extends CrudService<TResponse, any, any>> implements Resolve<TResponse | null> {
    
    constructor(@Inject('BaseApiService') protected service: TService, protected router: Router){}

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): MaybeAsync<TResponse | null> {
        return this.service.fetchSingle(route.params['id'])
        .pipe(
            map(response => response as TResponse),
            catchError(err => {
                this.router.navigate(['not-found']);
                return of(null);
            })
        );
    }
}