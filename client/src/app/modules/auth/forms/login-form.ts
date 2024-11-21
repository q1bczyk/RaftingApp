import { FormControl, FormGroup, Validators } from "@angular/forms";
import { FormFieldType, FormSettingType } from "../../shared/types/ui/form.type";
import { passwordStrengthValidator } from "./custom-validators/password-strength.validator";

const loginFormGroup: FormGroup = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required, Validators.minLength(8), passwordStrengthValidator()])
})

const loginFormFields : FormFieldType = {
    email : { label : "Email", fieldType : 'text', validationMessage : "Email jest wymagany"},
    password : { label : "Hasło", fieldType : 'password', validationMessage : "Hasło musi zawierać 8 znaków, duża literę, cyfrę i znak specjalny"}
}

export const loginForm: FormSettingType = {
    formGroup : loginFormGroup,
    fields : loginFormFields,
    buttonLabel : 'Zaloguj się' 
}


