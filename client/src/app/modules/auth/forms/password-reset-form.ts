import { FormControl, FormGroup, Validators } from "@angular/forms";
import { FormFieldType, FormSettingType } from "../../shared/types/ui/form.type";

const passwordResetFormGroup: FormGroup = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
})

const passwordResetFormFields : FormFieldType = {
    email : { label : "Email", fieldType : 'text', validationMessage : "Email jest wymagany"},
}

export const passwordResetForm: FormSettingType = {
    formGroup : passwordResetFormGroup,
    fields : passwordResetFormFields,
    buttonLabel : 'Resetuj hasło' 
}


