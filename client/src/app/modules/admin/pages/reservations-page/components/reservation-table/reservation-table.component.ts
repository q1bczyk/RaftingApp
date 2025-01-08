import { Component, Input, OnChanges, OnInit, SimpleChanges, WritableSignal } from '@angular/core';
import { ReservationService } from '../../../../../shared/services/api/reservation.service';
import { ApiManager } from '../../../../../core/api/api-manager';
import { SingleReservationDetailsType } from '../../../../../shared/types/api/reservation-types/reservation-details.type';
import { mapFiltersToQueryParams } from '../../../../helpers/filter-mapper';
import { ReservationFilterState } from '../filters/reservation-filter-state.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-reservation-table',
  standalone: true,
  imports: [],
  templateUrl: './reservation-table.component.html',
  styleUrl: './reservation-table.component.scss'
})
export class ReservationTableComponent implements OnChanges, OnInit{
  private filterSubscription!: Subscription;

  constructor(
    private service : ReservationService,
    private apiManager : ApiManager<SingleReservationDetailsType[]>,
    private filterState : ReservationFilterState
    ){
      
  }
  ngOnInit(): void {
    this.filterSubscription = this.filterState.getActiveFiltersObservable().subscribe(() => {
      this.fetchData(); 
    });
  }
  
  ngOnChanges(changes: SimpleChanges): void {
    if(changes['filters'] && !changes['filters'].firstChange) this.fetchData();
  }

  fetchData() : void{
    const filterUrl : string = mapFiltersToQueryParams(this.filterState.getActiveFilters());
    this.apiManager.exeApiRequest(this.service.fetchFilteredReservations(filterUrl), () => console.log(this.apiManager.data()))
  }

}
