import { Component } from '@angular/core';
import { FormComponent } from "../../../../../shared/ui/form/form.component";
import { ReservationFormCardComponent } from "../reservation-form-card/reservation-form-card.component";
import { BaseCreateComponent } from '../../../../../core/crud/base-create.directive';
import { GetEquipmentType } from '../../../../../shared/types/api/equipment-types/get-equipment.type';
import { ReservationDetailsType } from '../../../../types/api/reservation-details.type';
import { EquipmentService } from '../../../../../shared/services/api/equipment.service';
import { ModalFormService } from '../../../../../admin/services/ui/modal-form.service';
import { ApiManager } from '../../../../../core/api/api-manager';
import { dateSelectionForm } from '../../../../forms/date-selection-form';
import { FormGroup } from '@angular/forms';
import { mapFormToModel } from '../../../../../core/utils/mapper/mapper';
import { ReservationStateService } from '../../../services/states/reservation-state.service';
import { FormSettingType } from '../../../../../shared/types/ui/form.type';

@Component({
  selector: 'app-date-selection',
  standalone: true,
  imports: [FormComponent, ReservationFormCardComponent],
  templateUrl: './date-selection.component.html',
  styleUrl: './date-selection.component.scss'
})
export class DateSelectionComponent{
  form : FormSettingType = dateSelectionForm;
  
  constructor(private reservationStateService : ReservationStateService, private service : EquipmentService){}

  onFormSubmit(form: FormGroup): void {
    const mappedData: ReservationDetailsType = mapFormToModel(form);
    this.service.fetchAvaiableEquipment(mappedData)
      .subscribe(res => {
        this.reservationStateService.submitFirstStep(mappedData.participants, res);
      })
  };


}
