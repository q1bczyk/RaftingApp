import { Injectable, WritableSignal, signal } from "@angular/core";
import { GetSystemSettingsType } from "../../types/api/system-settings-types/get-system-settings.type";
import { SystemSettingsService } from "../api/system-settings.service";
import { ToastService } from "../ui/toasts/toast.service";

@Injectable({
    providedIn: 'root',
})
export class SystemSettingsHandlerService{
    private systemSettings : WritableSignal<GetSystemSettingsType | undefined> = signal(undefined);

    constructor(private systemSettingsService : SystemSettingsService, private toastSerivce : ToastService){}

    setSystemSettings(settings : GetSystemSettingsType) : void{
        this.systemSettings.set(settings);
    }

    getSystemSettings() : GetSystemSettingsType | undefined{
        return this.systemSettings();
    }

    updateDate(openDate : Date, closeDate : Date) : void{
        const currentSettings = this.systemSettings(); 
        if (currentSettings) {
            this.systemSettings.set({
                ...currentSettings,          
                seasonStartDate: openDate,
                seasonEndDate: closeDate        
            });
        }
        this.updateSettings();
    }

    private updateSettings() : void{
        const settingsToUpdate = this.systemSettings();
        if(!settingsToUpdate) return; 
        this.systemSettingsService.update(settingsToUpdate.id, settingsToUpdate)
            .subscribe(res => {
                this.systemSettings.set(res);
                this.toastSerivce.showToast('Pomyślnie zmieniono daty', 'success');
            })
    }

}