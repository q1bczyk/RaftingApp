import { Component, EventEmitter, Input, Output } from '@angular/core';
import { GetAccountType } from '../../../../../shared/types/api/account-types/get-account.type';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-account-item',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './account-item.component.html',
  styleUrl: './account-item.component.scss'
})
export class AccountItemComponent {
  @Input() account! : GetAccountType;
  @Input() index! : number;
  @Output() deleteEvent : EventEmitter<string> = new EventEmitter<string>();

  onDelete(email : string){
    this.deleteEvent.emit(email);
  }
}
