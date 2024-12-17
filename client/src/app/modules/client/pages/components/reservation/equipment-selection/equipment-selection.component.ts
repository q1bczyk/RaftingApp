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

  selectedParticipants: { [key: string]: number } = {};

  constructor(private reservationStateService: ReservationStateService) {
    this.initializeSelectedParticipants();
  }

  limitNotMet(equipmentMin: number): boolean {
    return this.reservationStateService.getParticipants().participantsLeft < equipmentMin;
  }

  generateRange(min: number, max: number): number[] {
    const range = [];
    for (let i = min; i <= max; i++)
      if(i <= this.reservationStateService.getParticipants().participantsLeft) range.push(i);
    return range;
  }

  getParticipants() : {participantsNumber : number, participantsLeft : number}{
    return this.reservationStateService.getParticipants();
  }

  onEquipmentSelect(equipment : GetEquipmentType) : void{
    this.reservationStateService.setParticipantLeft(Number(this.selectedParticipants[equipment.id]));
    const selectedEquipment : ReservationEquipmentType = {
      equipmentTypeId : equipment.id,
      participants : Number(this.selectedParticipants[equipment.id]),
      quantity : 1
    }
    this.reservationStateService.selectEquipment(selectedEquipment);
  }

  openMenu() : void{
    this.reservationStateService.setMenuState(true);
  }

  getAvaiableEquipment() : GetEquipmentType[]{
    return this.reservationStateService.getAvaiableEquipment();
  }

  private initializeSelectedParticipants(): void {
    this.getAvaiableEquipment().forEach(item => {
      this.selectedParticipants[item.id] = item.minParticipants;
    });
  }

}
