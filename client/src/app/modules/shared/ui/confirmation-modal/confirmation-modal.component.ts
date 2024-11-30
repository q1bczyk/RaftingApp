import { Component } from '@angular/core';
import { DialogModule } from 'primeng/dialog';
import { ConfirmationModalService } from '../../services/confiramtion-modal.service';

@Component({
  selector: 'app-confirmation-modal',
  standalone: true,
  imports: [],
  templateUrl: './confirmation-modal.component.html',
  styleUrl: './confirmation-modal.component.scss'
})
export class ConfirmationModalComponent {
  constructor(public confirmationModalService : ConfirmationModalService){}
}
