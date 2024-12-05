import { Component } from '@angular/core';
import { ModalFormService } from '../../services/ui/modal-form.service';

@Component({
  selector: 'app-form-modal',
  standalone: true,
  imports: [],
  templateUrl: './form-modal.component.html',
  styleUrl: './form-modal.component.scss'
})
export class FormModalComponent {
  constructor(public modalService: ModalFormService) { }

  onFormModalClick(event: Event): void {
    event.stopPropagation();
  }
}
