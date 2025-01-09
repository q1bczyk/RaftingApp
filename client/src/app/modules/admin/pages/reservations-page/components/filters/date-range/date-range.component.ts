import { JsonPipe } from '@angular/common';
import { Component, EventEmitter, Output, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { NgbCalendar, NgbDateParserFormatter, NgbDate, NgbDatepickerModule } from '@ng-bootstrap/ng-bootstrap';
import { ReservationFilterState } from '../reservation-filter-state.service';

@Component({
  selector: 'app-date-range',
  standalone: true,
  imports: [NgbDatepickerModule, FormsModule, JsonPipe],
  templateUrl: './date-range.component.html',
  styleUrl: './date-range.component.scss'
})
export class DateRangeComponent {
  calendar = inject(NgbCalendar);
  formatter = inject(NgbDateParserFormatter);

  constructor(private filterState : ReservationFilterState){}

  hoveredDate: NgbDate | null = null;
  fromDate: NgbDate | null = this.convertDateToNgbDate(this.filterState.getActiveFilters().specificDate);
  toDate: NgbDate | null = this.convertDateToNgbDate(this.filterState.getActiveFilters().endDate);

  onDateSelection(date: NgbDate) {
    if (!this.fromDate && !this.toDate) {
      this.fromDate = date;
      this.filterState.setSpecificDate(this.convertNgbDateToDate(this.fromDate))
    } else if (this.fromDate && !this.toDate && date && date.after(this.fromDate)) {
      this.toDate = date;
      this.filterState.setEndDate(this.convertNgbDateToDate(date))
      this.filterState.setStartDate(this.convertNgbDateToDate(this.fromDate))
      this.filterState.setSpecificDate(null);
    } else {
      this.toDate = null;
      this.filterState.setEndDate(null)
      this.filterState.setStartDate(null)
      this.fromDate = date;
      this.filterState.setSpecificDate(this.convertNgbDateToDate(this.fromDate))
    }
  }

  isHovered(date: NgbDate) {
    return (
      this.fromDate && !this.toDate && this.hoveredDate && date.after(this.fromDate) && date.before(this.hoveredDate)
    );
  }

  isInside(date: NgbDate) {
    return this.toDate && date.after(this.fromDate) && date.before(this.toDate);
  }

  isRange(date: NgbDate) {
    return (
      date.equals(this.fromDate) ||
      (this.toDate && date.equals(this.toDate)) ||
      this.isInside(date) ||
      this.isHovered(date)
    );
  }

  validateInput(currentValue: NgbDate | null, input: string): NgbDate | null {
    const parsed = this.formatter.parse(input);
    return parsed && this.calendar.isValid(NgbDate.from(parsed)) ? NgbDate.from(parsed) : currentValue;
  }

  private convertNgbDateToDate(ngbDate: NgbDate | null): Date | null {
    if (!ngbDate) return null; 
    return new Date(Date.UTC(ngbDate.year, ngbDate.month - 1, ngbDate.day)); // Użyj Date.UTC
}

  private convertDateToNgbDate(date: Date | null | undefined): NgbDate | null {
    if (!date) return null; 
    return new NgbDate(date.getFullYear(), date.getMonth() + 1, date.getDate()); // Miesiące są indeksowane od 0
  }

}
