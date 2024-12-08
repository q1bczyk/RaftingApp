import { FormControl, FormGroup, Validators } from "@angular/forms";
import { FormSettingType } from "../../shared/types/ui/form.type";
import { minMaxParticipantsValidator } from "./validators/minMaxParticipantsValidator";
import { addEquipmentFormFields } from "./add-qeuipment-form";

const editEquipmentFormGroup: FormGroup = new FormGroup(
  {
    typeName: new FormControl('', [Validators.required, Validators.minLength(3)]),
    minParticipants: new FormControl(1, [Validators.required, Validators.min(1)]),
    maxParticipants: new FormControl(1, [Validators.required, Validators.min(1)]),
    quantity: new FormControl(1, [Validators.required, Validators.min(0)]),
    pricePerPerson: new FormControl(50, [Validators.required, Validators.min(0)]),
    file: new FormControl(null), 
  },
  { validators: minMaxParticipantsValidator() }
);


export const editEquipmentForm: FormSettingType = {
  formGroup: editEquipmentFormGroup,
  fields: addEquipmentFormFields,
  buttonLabel: 'Edytuj',
};