import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, map } from "rxjs";
import { BaseApiService } from "../../../core/services/base-api.service";
import { DashboardDataType } from "../../types/api/dashboard-types/dashboard-data.type";

@Injectable({
    providedIn: 'root'
})

export class DashboardService extends BaseApiService
{
    constructor(protected override http : HttpClient){
        super(http, 'dashboard');
    }

    fetchDashboardData(): Observable<DashboardDataType> {
        return this.http.get<DashboardDataType>(this.url)
            .pipe(
                map(res => {
                    return res;
                })
            )
    }
}