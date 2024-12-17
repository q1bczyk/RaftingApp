import { Component } from '@angular/core';
import { ReservationFormCardComponent } from "../reservation-form-card/reservation-form-card.component";
import { EquipmentItemComponent } from "../../../../../shared/ui/equipment-item/equipment-item.component";
import { ReservationStateService } from '../../../services/states/reservation-state.service';
import { GetEquipmentType } from '../../../../../shared/types/api/equipment-types/get-equipment.type';
import { InputNumberModule } from 'primeng/inputnumber';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { SelectedEquipmentComponent } from "../selected-equipment/selected-equipment.component";
import { ReservationEquipmentType } from '../../../../../shared/types/api/reservation-types/make-reservation.type';

@Component({
  selector: 'app-equipment-selection',
  standalone: true,
  imports: [ReservationFormCardComponent, EquipmentItemComponent, InputNumberModule, CommonModule, FormsModule, SelectedEquipmentComponent],
  templateUrl: './equipment-selection.component.html',
  styleUrl: './equipment-selection.component.scss'
})
export class EquipmentSelectionComponent {

  equipment: GetEquipmentType[];
  participantsLeft: number;
  selectedParticipants: { [key: string]: number } = {};

  constructor(private reservationStateService: ReservationStateService) {
    this.equipment = reservationStateService.getAvaiableEquipment();
    this.participantsLeft = reservationStateService.getParticipants();
    this.initializeSelectedParticipants();
  }

  limitNotMet(equipmentMin: number): boolean {
    return this.participantsLeft < equipmentMin;
  }

  generateRange(min: number, max: number): number[] {
    const range = [];
    for (let i = min; i <= max; i++)
      if(i <= this.participantsLeft) range.push(i);
    return range;
  }

  getParticipants() : number{
    return this.reservationStateService.getParticipants();
  }

  onEquipmentSelect(equipment : GetEquipmentType) : void{
    this.participantsLeft -= Number(this.selectedParticipants[equipment.id])
    const selectedEquipment : ReservationEquipmentType = {
      equipmentTypeId : equipment.id,
      participants : Number(this.selectedParticipants[equipment.id]),
      quantity : 1
    }
    const selectedEquipmentIndex = this.equipment.findIndex(item => item.id === equipment.id);
    this.equipment[selectedEquipmentIndex].quantity -= 1 
    this.reservationStateService.selectEquipment(selectedEquipment);
  }

  openMenu() : void{
    this.reservationStateService.setMenuState(true);
  }

  private initializeSelectedParticipants(): void {
    this.equipment.forEach(item => {
      this.selectedParticipants[item.id] = item.minParticipants;
    });
  }

}
