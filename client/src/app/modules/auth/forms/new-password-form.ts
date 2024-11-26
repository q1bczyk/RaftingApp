import { FormControl, FormGroup, Validators } from "@angular/forms";
import { FormFieldType, FormSettingType } from "../../shared/types/ui/form.type";
import { passwordStrengthValidator } from "./custom-validators/password-strength.validator";
import { passwordMatchValidator } from "./custom-validators/password-match.validator";

const newPasswordFormGroup: FormGroup = new FormGroup({
        password: new FormControl('', [Validators.required, Validators.minLength(8), passwordStrengthValidator()]),
        confirmPassword: new FormControl('', [Validators.required, Validators.minLength(8), passwordStrengthValidator()])
    },
    { validators: passwordMatchValidator('password', 'confirmPassword') }
);

const newPasswordFormFields: FormFieldType = {
    password: { label: "Hasło", fieldType: 'password', validationMessage: "Hasło musi zawierać 8 znaków, duża literę, cyfrę i znak specjalny" },
    confirmPassword: { label: "Powtórz hasło", fieldType: 'password', validationMessage: "Hasło musi zawierać 8 znaków, duża literę, cyfrę i znak specjalny" }
}

export const newPasswordForm: FormSettingType = {
    formGroup: newPasswordFormGroup,
    fields: newPasswordFormFields,
    buttonLabel: 'Zmień hasło'
}


