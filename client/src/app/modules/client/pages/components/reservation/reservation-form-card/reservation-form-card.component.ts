import { Component } from '@angular/core';
import { BaseCreateComponent } from '../../../../../core/crud/base-create.directive';
import { SelectedEquipmentComponent } from "../selected-equipment/selected-equipment.component";
import { ReservationStateService } from '../../../services/states/reservation-state.service';

@Component({
  selector: 'app-reservation-form-card',
  standalone: true,
  imports: [SelectedEquipmentComponent],
  templateUrl: './reservation-form-card.component.html',
  styleUrl: './reservation-form-card.component.scss'
})
export class ReservationFormCardComponent{
  constructor(private reservationStateService : ReservationStateService){}

  getCurrentStep() : number{
    return this.reservationStateService.getCurrentStep();
  }

  isMenuOpen() : boolean{
    return this.reservationStateService.menuState();
  }
}
