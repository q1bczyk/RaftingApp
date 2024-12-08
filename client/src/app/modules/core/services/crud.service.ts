import { HttpClient } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";
import { BaseApiService } from "./base-api.service";
import { ICrudService } from "../interfaces/crud-service.interface";
import { Observable, map } from "rxjs";
import { ApiSuccessResponse } from "../types/api-success-response.type";

@Injectable({
    providedIn: 'root'
})

export class CrudService<TGet, TPost, TPut> extends BaseApiService implements ICrudService<TGet, TPost, TPut>
{
    constructor(protected override http : HttpClient, @Inject(String) controller : string){
        super(http, controller);
    }

    fetchSingle(id: string): Observable<TGet> {
        return this.http.get<TGet>(`${this.url}/${id}`)
            .pipe(
                map(res => {
                    return res;
                })
            )
    }

    create(data : TPost): Observable<TGet> {
        return this.http.post<TGet>(this.url, data)
            .pipe(
                map(res => {
                    return res;
                })
            )
    }

    fetchAll(): Observable<TGet[]> {
        return this.http.get<TGet[]>(this.url)
            .pipe(
                map(res => {
                    return res;
                })
            )
    }

    delete(id : string) : Observable<ApiSuccessResponse>{
        return this.http.delete<ApiSuccessResponse>(`${this.url}/${id}`)
        .pipe(
            map(res => {
                return res;
            })
        )
    }
}
