import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";

export function minMaxParticipantsValidator(): ValidatorFn {
  return (group: AbstractControl): ValidationErrors | null => {
    const min = group.get('minParticipants')?.value;
    const max = group.get('maxParticipants')?.value
    if ( min > max) return { minGreaterThanMax: true }; 
    return null; 
  };
}