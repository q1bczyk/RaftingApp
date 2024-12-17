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
  selectedEquipment : ReservationEquipmentType[]

  constructor(private reservationStateService : ReservationStateService){
    this.selectedEquipment = reservationStateService.getSelectedEquipment();
  }

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

  getSelectedEqName(eqId : string) : string{
    return this.reservationStateService.getSelectedEqName(eqId);
  }

  onUncheck(eqIndex : number) : void{
    console.log(eqIndex);
  }

}
