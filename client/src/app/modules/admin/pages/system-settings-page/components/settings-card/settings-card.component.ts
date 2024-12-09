import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-settings-card',
  standalone: true,
  imports: [],
  templateUrl: './settings-card.component.html',
  styleUrl: './settings-card.component.scss'
})
export class SettingsCardComponent {
  @Input() title! : string;
}
