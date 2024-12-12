import { FormControl, FormGroup, Validators } from "@angular/forms";
import { FormFieldType, FormSettingType } from "../../../shared/types/ui/form.type";

const contactSettingsFormGroup: FormGroup = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    phoneNumber: new FormControl('', [Validators.required, Validators.minLength(9)])
})

const contactSettingsFormFields : FormFieldType = {
    email : { label : "Email", fieldType : 'text', validationMessage : "Email jest wymagany"},
    phoneNumber : { label : "Numer telefonu", fieldType : 'text', validationMessage : "Numer telefonu musi mieć dokładnie 9 znaków"}
}

export const contactSettingsForm: FormSettingType = {
    formGroup : contactSettingsFormGroup,
    fields : contactSettingsFormFields,
    buttonLabel : 'Zapisz' 
}


