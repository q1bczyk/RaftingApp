import { HttpClient } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";
import { environment } from "../../../../../env/environment.prod";
import { BaseApiService } from "./base-api.service";
import { ICrudService } from "../interfaces/crud-service.interface";
import { Observable, map } from "rxjs";

@Injectable({
    providedIn: 'root'
})

export class CrudService<TGet, TPost, TPut> extends BaseApiService implements ICrudService<TGet, TPost, TPut>
{
    constructor(protected override http : HttpClient, @Inject(String) controller : string){
        super(http, controller);
    }

    fetchAll(): Observable<TGet[]> {
        return this.http.get<TGet[]>(this.url)
            .pipe(
                map(res => {
                    return res;
                })
            )
    }
}
