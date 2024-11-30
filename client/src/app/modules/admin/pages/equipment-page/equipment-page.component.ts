import { Component } from '@angular/core';
import { PageWrapperComponent } from "../../components/page-wrapper/page-wrapper.component";
import { BaseReadDirective } from '../../../core/crud/base-read.directive';
import { GetEquipmentType } from '../../../shared/types/api/equipment-types/get-equipment.type';
import { EquipmentService } from '../../../shared/services/api/equipment.service';
import { ApiManager } from '../../../core/api/api-manager';
import { EquipmentItemComponent } from '../../../shared/ui/equipment-item/equipment-item.component';

@Component({
  selector: 'app-equipment-page',
  standalone: true,
  imports: [PageWrapperComponent, EquipmentItemComponent],
  templateUrl: './equipment-page.component.html',
  styleUrl: './equipment-page.component.scss'
})
export class EquipmentPageComponent extends BaseReadDirective<GetEquipmentType, EquipmentService>{
  constructor(service : EquipmentService, apiManager : ApiManager<GetEquipmentType[]>){
    super(service, apiManager);
  }
}
