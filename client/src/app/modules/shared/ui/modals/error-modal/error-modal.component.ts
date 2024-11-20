import { Component } from '@angular/core';
import { ErrorModalService } from '../../../services/error-modal.service';

@Component({
  selector: 'app-error-modal',
  standalone: true,
  imports: [],
  templateUrl: './error-modal.component.html',
  styleUrl: './error-modal.component.scss'
})
export class ErrorModalComponent {
  constructor(public errorModalService : ErrorModalService){}
}
