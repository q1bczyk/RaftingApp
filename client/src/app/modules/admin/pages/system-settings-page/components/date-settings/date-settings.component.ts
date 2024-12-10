import { JsonPipe } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { NgbDatepickerModule, NgbTimepickerModule, NgbTimeStruct } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-date-settings',
  standalone: true,
  imports: [NgbTimepickerModule, JsonPipe, FormsModule, NgbDatepickerModule],
  templateUrl: './date-settings.component.html',
  styleUrl: './date-settings.component.scss'
})
export class DateSettingsComponent {
  time: NgbTimeStruct = { hour: 13, minute: 30, second: 0 };
}
