import { Component } from '@angular/core';
import { ToastService } from '../../services/ui/toasts/toast.service';

@Component({
  selector: 'app-toasts',
  standalone: true,
  imports: [],
  templateUrl: './toasts.component.html',
  styleUrl: './toasts.component.scss',
})
export class ToastsComponent {
  constructor(public toastService : ToastService){}

  renderIcon() : string{
    switch(this.toastService.getToastState()?.messageType){
      case 'success':
        return "bi bi-check-circle"
      case 'error':
        return 'bi bi-x-circle'
      case 'info':
          return "bi bi-info-circle"
      default:
        return ''
    }
  }

  closeToast() : void{
    this.toastService.closeToast();
  }

}
