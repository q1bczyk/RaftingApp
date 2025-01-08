import { Component, EventEmitter, Input, Output } from '@angular/core';
import { InputTextModule } from 'primeng/inputtext';
import { ButtonModule } from 'primeng/button';
import { FormSettingType } from '../../types/ui/form.type';
import { FormGroup, FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { LoadingService } from '../../services/loading.service';
import { InputNumberModule } from 'primeng/inputnumber';
import { CommonModule } from '@angular/common';
import { NgbDatepickerModule } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-form',
  standalone: true,
  imports: [InputTextModule, ButtonModule, ReactiveFormsModule, FormsModule, InputNumberModule, CommonModule, NgbDatepickerModule],
  templateUrl: './form.component.html',
  styleUrl: './form.component.scss'
})

export class FormComponent {
  @Input() formSettings: FormSettingType = { formGroup: new FormGroup({}), fields: {}, buttonLabel: '' }
  @Input() fullWidth: boolean | null = null;
  @Output() formSubmitEvent: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();

  uploadedFile: File | undefined = undefined;

  constructor(public loadingService: LoadingService) { }

  get controls(): string[] {
    return Object.keys(this.formSettings.formGroup.controls);
  }

  onFormSubmit(): void {
    if (this.formSettings.formGroup.invalid)
      return

    const dateControlValue = this.formSettings.formGroup.get('date')?.value;

    if (dateControlValue) {
      const convertedDate = new Date(dateControlValue.year, dateControlValue.month - 1, dateControlValue.day);
      this.formSettings.formGroup.patchValue({ ['date']: convertedDate });
    }

    this.formSubmitEvent.emit(this.formSettings.formGroup);
  }

  isFormInvalid(): boolean {
    const controls = this.formSettings.formGroup.controls;

    for (let control in controls) {
      if (controls[control]?.invalid) {
        return true;
      }
    }
    if (this.formSettings.formGroup.errors) return true;

    return false;
  }

  onUpload(event: Event) {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length > 0) {
      const file = input.files[0];
      this.uploadedFile = file;
      this.formSettings.formGroup.patchValue({ file: file });
    }
  }

}
