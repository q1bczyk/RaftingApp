import { Component } from '@angular/core';
import { DateRangeComponent } from "./date-range/date-range.component";
import { ReservationFilterState } from './reservation-filter-state.service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-filters',
  standalone: true,
  imports: [DateRangeComponent, FormsModule],
  templateUrl: './filters.component.html',
  styleUrl: './filters.component.scss'
})
export class FiltersComponent {
  lastname: string | null | undefined;
  reservationId: string | null | undefined; 

  constructor(private filterState : ReservationFilterState){
    this.lastname = filterState.getActiveFilters().lastNamePartial;
    this.reservationId = filterState.getActiveFilters().reservationId
  }

  onLastnameConfirm() : void{
    this.filterState.setLastname(this.lastname);
  }

  onReservationIdConfirm() : void{
    this.filterState.setId(this.reservationId);
  }
}
