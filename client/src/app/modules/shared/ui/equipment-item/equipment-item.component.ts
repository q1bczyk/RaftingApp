import { Component, Input } from '@angular/core';
import { GetEquipmentType } from '../../types/api/equipment-types/get-equipment.type';
import { DataViewModule} from 'primeng/dataview';
import { ButtonModule } from 'primeng/button';
import { TagModule } from 'primeng/tag';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-equipment-item',
  standalone: true,
  imports: [DataViewModule, ButtonModule, TagModule, CommonModule],
  templateUrl: './equipment-item.component.html',
  styleUrl: './equipment-item.component.scss'
})
export class EquipmentItemComponent {

  @Input() equipmentItem! : GetEquipmentType;
  @Input() index! : number;

  displayParticipants() : string{
    if(this.equipmentItem.maxParticipants === this.equipmentItem.minParticipants)
      return this.equipmentItem.minParticipants.toString();

    return this.equipmentItem.minParticipants + ' - ' + this.equipmentItem.maxParticipants;
  }

}
