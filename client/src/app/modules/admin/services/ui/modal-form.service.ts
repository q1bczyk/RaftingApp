import { Injectable, WritableSignal, signal } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ModalFormService {
  private modalState : WritableSignal<boolean> = signal(false);

  openModal() : void{
    this.modalState.set(true);
  }

  closeModal() : void{
    this.modalState.set(false);
  }

  isModalOpen() : boolean{
    return this.modalState();
  }
}
