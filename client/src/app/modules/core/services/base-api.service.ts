import { HttpClient } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";
import { environment } from "../../../../../env/environment.prod";

@Injectable({
    providedIn: 'root'
})

export class BaseApiService
{
    protected url : string;

    constructor(protected http : HttpClient, @Inject(String) controller : string){
        this.url = `${environment.apiUrl}/${controller}`;
    }
}
