import { Component, OnInit } from '@angular/core';
import { ReservationStateService } from '../../../services/states/reservation-state.service';
import { CommonModule } from '@angular/common';
import { ReservationEquipmentType } from '../../../../../shared/types/api/reservation-types/make-reservation.type';

@Component({
  selector: 'app-selected-equipment',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './selected-equipment.component.html',
  styleUrl: './selected-equipment.component.scss'
})
export class SelectedEquipmentComponent implements OnInit{
  isMenuOpen : boolean = false

  constructor(private reservationStateService : ReservationStateService){}

  ngOnInit(): void {
    setTimeout(() => {
      this.isMenuOpen = true;
    }, 10)
  }

  closeMenu() : void{
    this.isMenuOpen = false;
    setTimeout(() => {
      this.reservationStateService.setMenuState(false);
    }, 200)
  }

  getSelectedEquipment() : ReservationEquipmentType[]{
    return this.reservationStateService.getSelectedEquipment();
  }

  getSelectedEqName(eqId : string) : string{
    return this.reservationStateService.getSelectedEqName(eqId);
  }

  onUncheck(eqIndex : number, eqId : string, participants : number) : void{
    this.reservationStateService.uncheckEquipment(participants, eqId, eqIndex)
    if(this.getSelectedEquipment().length === 0) this.closeMenu();
  }

  displayPrice() : number{
    return this.reservationStateService.getBookingPrice();
  }

}
