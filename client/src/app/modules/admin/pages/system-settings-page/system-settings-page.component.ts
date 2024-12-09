import { Component } from '@angular/core';
import { PageWrapperComponent } from '../../components/page-wrapper/page-wrapper.component';
import { GetSystemSettingsType } from '../../../shared/types/api/system-settings-types/get-system-settings.type';
import { SystemSettingsHandlerService } from '../../../shared/services/others/system-settings-handler.service';
import { SettingsCardComponent } from "./components/settings-card/settings-card.component";
import { DateSettingsComponent } from "./components/date-settings/date-settings.component";

@Component({
  selector: 'app-system-settings-page',
  standalone: true,
  imports: [PageWrapperComponent, SettingsCardComponent, DateSettingsComponent],
  templateUrl: './system-settings-page.component.html',
  styleUrl: './system-settings-page.component.scss'
})
export class SystemSettingsPageComponent {

  systemSettings : GetSystemSettingsType | undefined;

  constructor(systemSettingsHandler : SystemSettingsHandlerService){
    this.systemSettings = systemSettingsHandler.getSystemSettings();
  }

}
