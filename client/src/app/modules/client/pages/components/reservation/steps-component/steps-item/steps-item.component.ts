import { Component, Input } from '@angular/core';
import { StepsType } from '../../../../../types/ui/steps.type';
import { ReservationStateService } from '../../../../services/states/reservation-state.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-steps-item',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './steps-item.component.html',
  styleUrl: './steps-item.component.scss'
})
export class StepsItemComponent {
  @Input() step! : StepsType;

  constructor(private reservationStateService : ReservationStateService){}

  getCurrentStep() : number{
    return this.reservationStateService.getCurrentStep();
  }
}
