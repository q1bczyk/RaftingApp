import { Injectable, WritableSignal, signal } from "@angular/core";
import { GetSystemSettingsType } from "../../types/api/system-settings-types/get-system-settings.type";
import { SystemSettingsService } from "../api/system-settings.service";
import { ToastService } from "../ui/toasts/toast.service";

@Injectable({
    providedIn: 'root',
})
export class SystemSettingsHandlerService {
    private systemSettings: WritableSignal<GetSystemSettingsType | undefined> = signal(undefined);

    constructor(private systemSettingsService: SystemSettingsService, private toastSerivce: ToastService) { }

    setSystemSettings(settings: GetSystemSettingsType): void {
        this.systemSettings.set(settings);
    }

    getSystemSettings(): GetSystemSettingsType | undefined {
        return this.systemSettings();
    }

    updateDate(openDate: Date, closeDate: Date): void {
        const currentSettings = this.systemSettings();
        if (currentSettings) {
            this.systemSettings.set({
                ...currentSettings,
                seasonStartDate: openDate,
                seasonEndDate: closeDate
            });
        }
        this.updateSettings("Pomyślnie zaktualizowano daty");
    }

    updateContact(email: string, phoneNumber: number) {
        const currentSettings = this.systemSettings();
        if (currentSettings) {
            this.systemSettings.set({
                ...currentSettings,
                email: email,
                phoneNumber: phoneNumber,
            });
        }
        this.updateSettings('Pomyślnie zaktualizowano dane kontaktowe');
    }

    updateReservationTerms(dayEarliestBookingTime: number, dayLatestBookingTime: number, hoursRentalTime: number) {
        const currentSettings = this.systemSettings();
        if (currentSettings) {
            this.systemSettings.set({
                ...currentSettings,
                dayEarliestBookingTime: dayEarliestBookingTime,
                dayLatestBookingTime: dayLatestBookingTime,
                hoursRentalTime: hoursRentalTime
            });
        }
        this.updateSettings('Pomyślnie zaktualizowano warunki rezerwacji');
    }

    private updateSettings(successMessage: string): void {
        const settingsToUpdate = this.systemSettings();
        if (!settingsToUpdate) return;
        this.systemSettingsService.update(settingsToUpdate.id, settingsToUpdate)
            .subscribe(res => {
                this.systemSettings.set(res);
                this.toastSerivce.showToast(successMessage, 'success');
            })
    }

}