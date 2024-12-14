import { Component } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { StepsModule } from 'primeng/steps';
import { StepsComponentComponent } from "./steps-component/steps-component.component";
import { DateSelectionComponent } from "./date-selection/date-selection.component";

@Component({
  selector: 'app-reservation',
  standalone: true,
  imports: [StepsModule, StepsComponentComponent, DateSelectionComponent],
  templateUrl: './reservation.component.html',
  styleUrl: './reservation.component.scss'
})
export class ReservationComponent {

}
