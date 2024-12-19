import { FormControl, FormGroup, Validators } from "@angular/forms";
import { FormFieldType, FormSettingType } from "../../shared/types/ui/form.type";

const bookerDataFormGroup: FormGroup = new FormGroup({
    bookerName : new FormControl('', [Validators.required, Validators.minLength(2)]),
    bookerLastname : new FormControl('', [Validators.required, Validators.minLength(2)]),
    bookerEmail : new FormControl('', [Validators.required, Validators.email]),
    bookerPhoneNumber : new FormControl('', [Validators.required, Validators.minLength(9), Validators.maxLength(9)])
})

const bookerDataFormFields : FormFieldType = {
    bookerName : { label : "Imię", fieldType : 'text', validationMessage : "Imię jest wymagane"},
    bookerLastname : { label : "Nazwisko", fieldType : 'text', validationMessage : "Nazwisko jest wymagane"},
    bookerEmail : { label : "Email", fieldType : 'text', validationMessage : "Email jest wymagany"},
    bookerPhoneNumber : { label : "Number telefonu", fieldType : 'text', validationMessage : "Number jest wymagany"},
}

export const bookerDataForm: FormSettingType = {
    formGroup : bookerDataFormGroup,
    fields : bookerDataFormFields,
    buttonLabel : 'Potwierdź' 
}


