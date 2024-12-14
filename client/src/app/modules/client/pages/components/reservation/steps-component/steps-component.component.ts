import { Component } from '@angular/core';
import { StepsType } from '../../../../types/ui/steps.type';
import { stepsItems } from './steps-items';
import { StepsItemComponent } from './steps-item/steps-item.component';

@Component({
  selector: 'app-steps-component',
  standalone: true,
  imports: [StepsItemComponent],
  templateUrl: './steps-component.component.html',
  styleUrl: './steps-component.component.scss'
})
export class StepsComponentComponent {
  steps : StepsType[] = stepsItems;



}
