import { Component } from '@angular/core';
import { GetEquipmentType } from '../../../../../shared/types/api/equipment-types/get-equipment.type';
import { CreateEquipmentType } from '../../../../../shared/types/api/equipment-types/create-equipment.type';
import { EquipmentService } from '../../../../../shared/services/api/equipment.service';
import { ApiManager } from '../../../../../core/api/api-manager';
import { LoadingService } from '../../../../../shared/services/loading.service';
import { BaseUpdateComponent } from '../../../../../core/crud/base-update.directive';
import { ToastService } from '../../../../../shared/services/ui/toasts/toast.service';
import { FormComponent } from "../../../../../shared/ui/form/form.component";
import { editEquipmentForm } from '../../../../forms/edit-equipment-form';
import { UpdateEquipmentType } from '../../../../../shared/types/api/equipment-types/update-equipment.type';
import { createFormData } from '../../../../../core/utils/formData/createFormData';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'app-edit-equipment',
  standalone: true,
  imports: [FormComponent],
  templateUrl: './edit-equipment.component.html',
  styleUrl: './edit-equipment.component.scss',
  providers: [ApiManager],
})
export class EditEquipmentComponent extends BaseUpdateComponent<GetEquipmentType, UpdateEquipmentType, EquipmentService>{
  constructor(
        apiManager : ApiManager<GetEquipmentType>, 
        updateApiManager : ApiManager<GetEquipmentType>,
        service : EquipmentService,
        toastService : ToastService,
        loadingService : LoadingService,
  ){
    super(apiManager, updateApiManager, service, toastService, loadingService, editEquipmentForm)
  }

  override onFormSubmit(form : FormGroup) : void{
    const mappedData : FormData = createFormData(form);
    this.apiManager.exeApiRequest(this.service.update(this.id, mappedData), () => this.onSuccessEdit())
  };
}
