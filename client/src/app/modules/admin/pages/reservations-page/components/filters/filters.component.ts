import { Component, ViewChild } from '@angular/core';
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

  @ViewChild(DateRangeComponent) dateRangeComponent!: DateRangeComponent;

  constructor(private filterState : ReservationFilterState){
    this.lastname = filterState.getActiveFilters().lastNamePartial;
    this.reservationId = filterState.getActiveFilters().reservationId
  }

  onClear() : void{
    this.filterState.onClear();
    this.lastname = null;      // Resetowanie lokalnych zmiennych w komponencie
    this.reservationId = null;
    if (this.dateRangeComponent) this.dateRangeComponent.resetDates();
  }

  onFilter() : void{
    this.filterState.setLastname(this.lastname);
    this.filterState.setId(this.reservationId);
    this.filterState.notifyFiltersChanged();
  }
}
