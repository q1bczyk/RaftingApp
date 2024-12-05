import { Component } from '@angular/core';
import { BaseCreateComponent } from '../../../../../core/crud/base-create.directive';
import { GetEquipmentType } from '../../../../../shared/types/api/equipment-types/get-equipment.type';
import { CreateEquipmentType } from '../../../../../shared/types/api/equipment-types/create-equipment.type';
import { EquipmentService } from '../../../../../shared/services/api/equipment.service';
import { ApiManager } from '../../../../../core/api/api-manager';
import { addEquipmentForm } from '../../../../forms/add-qeuipment-form';
import { ModalFormService } from '../../../../services/ui/modal-form.service';
import { FormComponent } from "../../../../../shared/ui/form/form.component";
import { FormGroup } from '@angular/forms';
import { createFormData } from '../../../../../core/utils/formData/createFormData';
import { ToastService } from '../../../../../shared/services/ui/toasts/toast.service';

@Component({
  selector: 'app-add-equipment',
  standalone: true,
  imports: [FormComponent],
  templateUrl: './add-equipment.component.html',
  styleUrl: './add-equipment.component.scss'
})
export class AddEquipmentComponent extends BaseCreateComponent<GetEquipmentType, CreateEquipmentType, EquipmentService>{

  constructor(
    modalFormService : ModalFormService,
    apiManager : ApiManager<GetEquipmentType>, 
    service : EquipmentService){
    super(modalFormService, service, apiManager, addEquipmentForm);
  }

  override onFormSubmit(form : FormGroup) : void{
    const mappedData : FormData = createFormData(form);
    this.apiManager.exeApiRequest(this.service.create(mappedData), () => this.onSuccess())
};

}
