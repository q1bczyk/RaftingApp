import { FormControl, FormGroup, Validators } from "@angular/forms";
import { FormFieldType, FormSettingType } from "../../shared/types/ui/form.type";

const dateSelectionFormGroup: FormGroup = new FormGroup({
    date: new FormControl('', [Validators.required]),
    participants: new FormControl('', [Validators.required, Validators.minLength(9)])
})

const dateSelectionFormFields : FormFieldType = {
    date : { label : "Data spływu", fieldType : 'date', validationMessage : "Data jest wymagana"},
    participants : { label : "Ilość osób", fieldType : 'number', validationMessage : "Minimalna ilość osób to 1", extraNumberFields: {minValue: 1}}
}

export const dateSelectionForm: FormSettingType = {
    formGroup : dateSelectionFormGroup,
    fields : dateSelectionFormFields,
    buttonLabel : 'Zatwierdź' 
}


