import { Component, Input } from '@angular/core';
import { GetEquipmentType } from '../../../../../shared/types/api/equipment-types/get-equipment.type';

@Component({
  selector: 'app-available-equipment',
  standalone: true,
  imports: [],
  templateUrl: './available-equipment.component.html',
  styleUrl: './available-equipment.component.scss'
})
export class AvailableEquipmentComponent {
  @Input() equipment! : GetEquipmentType[]
}
