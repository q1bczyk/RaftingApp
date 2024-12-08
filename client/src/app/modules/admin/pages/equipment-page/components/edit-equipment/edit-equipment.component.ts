import { Component } from '@angular/core';
import { BaseReadSingleComponent } from '../../../../../core/crud/base-read-single.directive';
import { GetEquipmentType } from '../../../../../shared/types/api/equipment-types/get-equipment.type';
import { CreateEquipmentType } from '../../../../../shared/types/api/equipment-types/create-equipment.type';
import { EquipmentService } from '../../../../../shared/services/api/equipment.service';
import { ModalFormService } from '../../../../services/ui/modal-form.service';
import { ApiManager } from '../../../../../core/api/api-manager';
import { ConfirmationModalService } from '../../../../../shared/services/confiramtion-modal.service';
import { ToastService } from '../../../../../shared/services/ui/toasts/toast.service';
import { LoadingService } from '../../../../../shared/services/loading.service';

@Component({
  selector: 'app-edit-equipment',
  standalone: true,
  imports: [],
  templateUrl: './edit-equipment.component.html',
  styleUrl: './edit-equipment.component.scss'
})
export class EditEquipmentComponent extends BaseReadSingleComponent<GetEquipmentType, CreateEquipmentType, EquipmentService>{

  constructor(
    modalFormSerice : ModalFormService,
    service : EquipmentService,
    apiMenager : ApiManager<GetEquipmentType>,
    confirmationModal : ConfirmationModalService,
    loadingService : LoadingService,
  ){
    super(modalFormSerice, service, apiMenager, confirmationModal, loadingService);
  }

}
