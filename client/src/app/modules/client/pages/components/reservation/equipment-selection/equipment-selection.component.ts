import { Component } from '@angular/core';
import { ReservationFormCardComponent } from "../reservation-form-card/reservation-form-card.component";
import { EquipmentItemComponent } from "../../../../../shared/ui/equipment-item/equipment-item.component";
import { ReservationStateService } from '../../../services/states/reservation-state.service';
import { GetEquipmentType } from '../../../../../shared/types/api/equipment-types/get-equipment.type';
import { InputNumberModule } from 'primeng/inputnumber';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-equipment-selection',
  standalone: true,
  imports: [ReservationFormCardComponent, EquipmentItemComponent, InputNumberModule, CommonModule],
  templateUrl: './equipment-selection.component.html',
  styleUrl: './equipment-selection.component.scss'
})
export class EquipmentSelectionComponent {

  equipment: GetEquipmentType[];
  participants: number;
  participantsLeft: number;
  selectedParticipants: { [key: string]: number } = {};

  constructor(private reservationStateService: ReservationStateService) {
    this.equipment = reservationStateService.getAvaiableEquipment();
    this.participants = reservationStateService.getParticipants();
    this.participantsLeft = reservationStateService.getParticipants();
    this.initializeSelectedParticipants();
  }

  onSelect(equipmentId: string): void {

  }

  limitNotMet(equipmentMin: number): boolean {
    return this.participantsLeft < equipmentMin;
  }

  onParticipantsChange(equipmentId : string) : void{

  }

  generateRange(min: number, max: number): number[] {
    const range = [];
    for (let i = min; i <= max; i++)
      if(i <= this.participantsLeft) range.push(i);
    return range;
  }

  private initializeSelectedParticipants(): void {
    this.equipment.forEach(item => {
      this.selectedParticipants[item.id] = item.minParticipants;
    });
  }

}
