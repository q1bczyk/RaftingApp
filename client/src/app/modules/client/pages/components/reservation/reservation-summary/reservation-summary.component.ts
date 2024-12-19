import { Component } from '@angular/core';
import { ReservationComponent } from "../reservation.component";
import { ReservationFormCardComponent } from "../reservation-form-card/reservation-form-card.component";
import { SelectedEquipmentComponent } from "../selected-equipment/selected-equipment.component";
import { ReservationStateService } from '../../../services/states/reservation-state.service';
import { MakeReservationType, ReservationEquipmentType } from '../../../../../shared/types/api/reservation-types/make-reservation.type';
import { format } from 'date-fns';
import { dateFormat } from '../../../../../core/date-format/date-format';
import { FormSettingType } from '../../../../../shared/types/ui/form.type';
import { bookerDataForm } from '../../../../forms/booker-data-form';
import { FormComponent } from "../../../../../shared/ui/form/form.component";
import { FormGroup } from '@angular/forms';
import { mapFormToModel } from '../../../../../core/utils/mapper/mapper';
import { BookerType } from '../../../../types/ui/booker-type';

@Component({
  selector: 'app-reservation-summary',
  standalone: true,
  imports: [ReservationComponent, ReservationFormCardComponent, SelectedEquipmentComponent, FormComponent],
  templateUrl: './reservation-summary.component.html',
  styleUrl: './reservation-summary.component.scss'
})
export class ReservationSummaryComponent {
  reservation : MakeReservationType;
  form : FormSettingType = bookerDataForm;

  constructor(private reservationStateService : ReservationStateService){
    this.reservation = this.reservationStateService.getReservationDetails();
  }

  getPrice() : number{
    return this.reservationStateService.getBookingPrice();
  }

  getSelectedEquipment(): ReservationEquipmentType[] {
    const equipmentMap: { [key: string]: ReservationEquipmentType } = {};
  
    this.reservation.reservationEquipment.forEach(item => {
      if (equipmentMap[item.equipmentTypeId]) {
        equipmentMap[item.equipmentTypeId].quantity += item.quantity;
        equipmentMap[item.equipmentTypeId].participants += item.participants;
      } else 
        equipmentMap[item.equipmentTypeId] = { ...item };
    });
    return Object.values(equipmentMap);
  }

  getSummary() : {totalPrice : number, totalParticipants : number, totalEq : number, date : string}{
    const totalParticipants = this.reservation.reservationEquipment.reduce((acc, curr) => acc + curr.participants, 0);
    const totalEq = this.reservation.reservationEquipment.reduce((acc, curr) => acc + curr.quantity, 0)
    const date : string = format(this.reservation.executionDate, dateFormat);

      return{
        totalPrice : this.reservationStateService.getBookingPrice(),
        totalParticipants : totalParticipants,
        totalEq : totalEq,
        date : date
      }
  }

  getSelectedEqName(id : string) : string{
    return this.reservationStateService.getSelectedEqName(id);
  }

  onFormSubmit(data : FormGroup){
    const mappedData : BookerType = mapFormToModel(data);
    const convertedEquipment : ReservationEquipmentType[] = this.getSelectedEquipment();
    this.reservationStateService.makeReservation(mappedData, convertedEquipment);
    this.reservationStateService.setStep(4);
  }

  back() : void{
    this.reservationStateService.setStep(2);
  }
}
