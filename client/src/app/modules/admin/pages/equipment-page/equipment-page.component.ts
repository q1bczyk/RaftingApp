import { Component } from '@angular/core';
import { PageWrapperComponent } from "../../components/page-wrapper/page-wrapper.component";
import { BaseReadDirective } from '../../../core/crud/base-read.directive';
import { GetEquipmentType } from '../../../shared/types/api/equipment-types/get-equipment.type';
import { EquipmentService } from '../../../shared/services/api/equipment.service';
import { ApiManager } from '../../../core/api/api-manager';
import { EquipmentItemComponent } from '../../../shared/ui/equipment-item/equipment-item.component';
import { ConfirmationModalService } from '../../../shared/services/confiramtion-modal.service';
import { ApiSuccessResponse } from '../../../core/types/api-success-response.type';

@Component({
  selector: 'app-equipment-page',
  standalone: true,
  imports: [PageWrapperComponent, EquipmentItemComponent],
  templateUrl: './equipment-page.component.html',
  styleUrl: './equipment-page.component.scss'
})
export class EquipmentPageComponent extends BaseReadDirective<GetEquipmentType, EquipmentService>{
  constructor(
    service : EquipmentService, 
    apiManager : ApiManager<GetEquipmentType[]>, 
    confirmationModalService : ConfirmationModalService,
    protected apiDeletemanager : ApiManager<ApiSuccessResponse>){
    super(service, apiManager, confirmationModalService, apiDeletemanager);
  }

  onDeleteEvent(equipmentId : string) : void{
    this.confirmationModalService.openModal(() => this.deleteApiRequest(equipmentId));
  }
}
