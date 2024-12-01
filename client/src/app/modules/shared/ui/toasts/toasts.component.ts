import { Component } from '@angular/core';
import { ToastService } from '../../services/ui/toasts/toast.service';

@Component({
  selector: 'app-toasts',
  standalone: true,
  imports: [],
  templateUrl: './toasts.component.html',
  styleUrl: './toasts.component.scss'
})
export class ToastsComponent {
  constructor(public toastService : ToastService){}

  renderIcon() : string{
    switch(this.toastService.getToastState()?.messageType){
      case 'success':
        return "bi bi-check-circle"
        break;
      case 'error':
        return 'bi bi-x-circle'
        break;
      case 'info':
          return "bi bi-info-circle"
          break;
      default:
        return ''
    }
  }

  closeToast() : void{
    this.toastService.closeToast();
  }

}
