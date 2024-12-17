import { Component } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { StepsModule } from 'primeng/steps';
import { StepsComponentComponent } from "./steps-component/steps-component.component";
import { DateSelectionComponent } from "./date-selection/date-selection.component";
import { ReservationStateService } from '../../services/states/reservation-state.service';
import { EquipmentSelectionComponent } from "./equipment-selection/equipment-selection.component";
import { SelectedEquipmentComponent } from "./selected-equipment/selected-equipment.component";

@Component({
  selector: 'app-reservation',
  standalone: true,
  imports: [StepsModule, StepsComponentComponent, DateSelectionComponent, EquipmentSelectionComponent, SelectedEquipmentComponent],
  templateUrl: './reservation.component.html',
  styleUrl: './reservation.component.scss'
})
export class ReservationComponent {

  constructor(private reservationStateService : ReservationStateService){}

  getCurrentStep() : number{
    return this.reservationStateService.getCurrentStep();
  } 

}
