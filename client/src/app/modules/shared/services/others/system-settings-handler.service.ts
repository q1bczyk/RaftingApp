import { Injectable, WritableSignal, signal } from "@angular/core";
import { GetSystemSettingsType } from "../../types/api/system-settings-types/get-system-settings.type";

@Injectable({
    providedIn: 'root',
})
export class SystemSettingsHandlerService{
    private systemSettings : WritableSignal<GetSystemSettingsType | undefined> = signal(undefined);

    setSystemSettings(settings : GetSystemSettingsType) : void{
        this.systemSettings.set(settings);
    }

}