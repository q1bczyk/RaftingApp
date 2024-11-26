import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';


/**
 * @param passwordField - The name of the password field.
 * @param confirmPasswordField - The name of the confirm password field.
 * @returns ValidatorFn
 */

export function passwordMatchValidator(passwordField: string, confirmPasswordField: string): ValidatorFn {
  return (formGroup: AbstractControl): ValidationErrors | null => {
    const password = formGroup.get(passwordField)?.value;
    const confirmPassword = formGroup.get(confirmPasswordField)?.value;

    if (password !== confirmPassword) {
      formGroup.get(confirmPasswordField)?.setErrors({ passwordMismatch: true });
      return { passwordMismatch: true }; 
    }

    formGroup.get(confirmPasswordField)?.setErrors(null);
    return null;
  };
}