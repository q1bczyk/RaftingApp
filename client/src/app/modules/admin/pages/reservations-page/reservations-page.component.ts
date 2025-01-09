import { Component } from '@angular/core';
import { PageWrapperComponent } from "../../components/page-wrapper/page-wrapper.component";
import { FiltersComponent } from './components/filters/filters.component';
import { ReservationTableComponent } from "./components/reservation-table/reservation-table.component";

@Component({
  selector: 'app-reservations-page',
  standalone: true,
  imports: [PageWrapperComponent, ReservationTableComponent, FiltersComponent],
  templateUrl: './reservations-page.component.html',
  styleUrl: './reservations-page.component.scss'
})
export class ReservationsPageComponent {
 
}
