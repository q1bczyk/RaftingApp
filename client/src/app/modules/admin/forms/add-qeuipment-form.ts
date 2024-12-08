import { FormControl, FormGroup, Validators } from "@angular/forms"
import { FormFieldType, FormSettingType } from "../../shared/types/ui/form.type"
import { minMaxParticipantsValidator } from "./validators/minMaxParticipantsValidator";

const addEquipmentFormGroup: FormGroup = new FormGroup(
    {
      typeName: new FormControl('', [Validators.required, Validators.minLength(3)]),
      minParticipants: new FormControl(1, [Validators.required, Validators.min(1)]),
      maxParticipants: new FormControl(1, [Validators.required, Validators.min(1)]),
      quantity: new FormControl(1, [Validators.required, Validators.min(0)]),
      pricePerPerson: new FormControl(50, [Validators.required, Validators.min(0)]),
      file: new FormControl(null, Validators.required),
    },
    { validators: minMaxParticipantsValidator() }
  );

export const addEquipmentFormFields: FormFieldType = {
    typeName: {
      label: "Nazwa",
      fieldType: 'text',
      validationMessage: "Nazwa jest wymagana"
    },
    minParticipants: {
      label: "Wymagana liczba osób",
      fieldType: 'number',
      validationMessage: "Minimalna liczba osób to 1",
      extraNumberFields: {
        minValue: 1,
      }
    },
    maxParticipants: {
      label: "Maksymalna liczba osób",
      fieldType: 'number',
      validationMessage: "Maksymalna liczba osób nie może być mniejsza od minimalnej",
      extraNumberFields: {
        minValue: 1,
      }
    },
    quantity: {
      label: "Ilość sprzętu",
      fieldType: 'number',
      validationMessage: "Ilość sprzętu nie może być mniejsza od 0",
      extraNumberFields: {
        minValue: 0,
      }
    },
    pricePerPerson: {
      label: "Cena od osoby",
      fieldType: 'number',
      validationMessage: "Cena nie może być mniejsza od 0",
      extraNumberFields: {
        minValue: 0,
        currency: 'PLN'
      }
    },
    file: {
      label: "Zdjęcie",
      fieldType: 'file',
      validationMessage: "Zdjęcie jest wymagane"
    },
  };

export const addEquipmentForm: FormSettingType = {
    formGroup : addEquipmentFormGroup,
    fields : addEquipmentFormFields,
    buttonLabel : 'Dodaj' 
}


