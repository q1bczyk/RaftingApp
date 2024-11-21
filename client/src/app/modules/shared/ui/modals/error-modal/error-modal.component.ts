import { Component } from '@angular/core';
import { ErrorService } from '../../../services/error.service';

@Component({
  selector: 'app-error-modal',
  standalone: true,
  imports: [],
  templateUrl: './error-modal.component.html',
  styleUrl: './error-modal.component.scss'
})
export class ErrorModalComponent {
  constructor(public errorModalService : ErrorService){}
}
