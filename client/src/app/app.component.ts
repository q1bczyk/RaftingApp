import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { ErrorModalComponent } from "./modules/shared/ui/modals/error-modal/error-modal.component";
import { ErrorService } from './modules/shared/services/error.service';
import { ConfirmationModalComponent } from './modules/shared/ui/confirmation-modal/confirmation-modal.component';
import { ConfirmationModalService } from './modules/shared/services/confiramtion-modal.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, CommonModule, ErrorModalComponent, ConfirmationModalComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent {
  title = 'client';

  constructor(public errorService : ErrorService, public confirmationModalService : ConfirmationModalService){}
}
