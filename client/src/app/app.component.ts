import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { ErrorModalComponent } from "./modules/shared/ui/modals/error-modal/error-modal.component";
import { ErrorService } from './modules/shared/services/error.service';
import { ConfirmationModalComponent } from './modules/shared/ui/confirmation-modal/confirmation-modal.component';
import { ConfirmationModalService } from './modules/shared/services/confiramtion-modal.service';
import { ToastService } from './modules/shared/services/ui/toasts/toast.service';
import { ToastsComponent } from "./modules/shared/ui/toasts/toasts.component";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, CommonModule, ErrorModalComponent, ConfirmationModalComponent, ToastsComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent {
  title = 'client';

  constructor(
    public errorService : ErrorService, 
    public confirmationModalService : ConfirmationModalService,
    public toastService : ToastService){}
}
