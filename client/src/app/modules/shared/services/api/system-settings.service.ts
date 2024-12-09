import { Injectable, WritableSignal, signal } from "@angular/core";
import { CrudService } from "../../../core/services/crud.service";
import { GetSystemSettingsType } from "../../types/api/system-settings-types/get-system-settings.type";
import { BaseSystemSettingsType } from "../../types/api/system-settings-types/base-system-settings-type";
import { HttpClient } from "@angular/common/http";
import { Observable, map } from "rxjs";

@Injectable({
    providedIn: 'root',
})
export class SystemSettingsService extends CrudService<GetSystemSettingsType, BaseSystemSettingsType, BaseSystemSettingsType>
{
    constructor(protected override http : HttpClient){
        super(http, 'settings');
    }

    override fetchSingle(): Observable<GetSystemSettingsType> {
        return this.http.get<GetSystemSettingsType>(`${this.url}`)
            .pipe(
                map(res => {
                    return res;
                })
            )
    }
}