import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-payment-tabs',
  standalone: true,
  imports: [],
  templateUrl: './payment-tabs.component.html',
  styleUrl: './payment-tabs.component.scss'
})
export class PaymentTabsComponent {
  @Input() selectedMethod! : number; 
  @Output() methodChangeEvent : EventEmitter<number> = new EventEmitter<number>();

  onMethodChange(methodId : number) : void {
    this.methodChangeEvent.emit(methodId);
  }

}
