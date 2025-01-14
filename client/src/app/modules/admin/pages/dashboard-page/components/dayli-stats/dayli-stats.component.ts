import { Component, Input } from '@angular/core';
import { SingleReservationDetailsType } from '../../../../../shared/types/api/reservation-types/reservation-details.type';
import { GetEquipmentType } from '../../../../../shared/types/api/equipment-types/get-equipment.type';

interface StatsType{
  reservations : number,
  clients : number,
  price : number,
  equipment : number,
}

@Component({
  selector: 'app-dayli-stats',
  standalone: true,
  imports: [],
  templateUrl: './dayli-stats.component.html',
  styleUrl: './dayli-stats.component.scss'
})
export class DayliStatsComponent {
  @Input() reservations! : SingleReservationDetailsType[] 
  @Input() availableEquipment! : GetEquipmentType[]

  calcStats() : StatsType{
      const stats : StatsType = {
        reservations : this.reservations.length,
        clients: this.reservations.reduce((total, reservation) => total + reservation.participantNumber, 0) || 0,
        price: this.reservations.reduce((total, reservation) => total + reservation.bookPrice, 0) || 0,
        equipment: this.availableEquipment.reduce((total, equipment) => total + equipment.maxParticipants * equipment.quantity, 0) || 0,
      }

      return stats;
  }
}
