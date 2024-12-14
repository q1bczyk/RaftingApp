import { Component } from '@angular/core';
import { FormSettingType } from '../../../../../shared/types/ui/form.type';
import { dateSelectionForm } from '../../../../forms/date-selection-form';
import { FormComponent } from "../../../../../shared/ui/form/form.component";

@Component({
  selector: 'app-date-selection',
  standalone: true,
  imports: [FormComponent],
  templateUrl: './date-selection.component.html',
  styleUrl: './date-selection.component.scss'
})
export class DateSelectionComponent {
  dateSelectionForm : FormSettingType = dateSelectionForm; 
}
