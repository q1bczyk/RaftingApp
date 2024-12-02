import { FormGroup } from "@angular/forms";

export interface FormSettingType{
    formGroup : FormGroup,
    fields : FormFieldType,
    buttonLabel : string,    
}

export interface FormFieldType{
    [key : string ] : { 
        label: string, 
        fieldType : string, 
        validationMessage? : string
        extraNumberFields? : {
            currency? : string | null, 
            minValue : number | null, 
            maxValue? : number | null
        }}
    
}