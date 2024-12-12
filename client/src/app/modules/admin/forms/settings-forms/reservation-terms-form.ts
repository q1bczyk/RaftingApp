import { FormControl, FormGroup, Validators } from "@angular/forms";
import { FormFieldType, FormSettingType } from "../../../shared/types/ui/form.type";

const reservationTermsFormGroup: FormGroup = new FormGroup({
    dayEarliestBookingTime: new FormControl('', [Validators.required]),
    dayLatestBookingTime: new FormControl('', [Validators.required]),
    hoursRentalTime: new FormControl('', [Validators.required])
})

const reservationTermsFormFields : FormFieldType = {
    dayEarliestBookingTime: { 
        label : "Minimalne wyprzedzenie(dni)", 
        fieldType : 'number', 
        validationMessage : "Minimalna wartość to 10",
        extraNumberFields: {
            minValue: 10,
          }
    },
    dayLatestBookingTime: { 
        label : "Maksymalne wyprzedzenie(dni)", 
        fieldType : 'number', 
        validationMessage : "Minimalna wartość to 1",
        extraNumberFields: {
            minValue: 1,
          }
    },
    hoursRentalTime: { 
        label : "Czas wypożyczenia sprzętu(godziny)", 
        fieldType : 'number', 
        validationMessage : "Minimalna wartość to 1",
        extraNumberFields: {
            minValue: 1,
          }
    },
   
}

export const reservationTermsForm: FormSettingType = {
    formGroup : reservationTermsFormGroup,
    fields : reservationTermsFormFields,
    buttonLabel : 'Zapisz' 
}


