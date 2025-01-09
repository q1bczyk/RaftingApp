import { Component, Input, OnChanges, OnInit, SimpleChanges, WritableSignal } from '@angular/core';
import { ReservationService } from '../../../../../shared/services/api/reservation.service';
import { ApiManager } from '../../../../../core/api/api-manager';
import { SingleReservationDetailsType } from '../../../../../shared/types/api/reservation-types/reservation-details.type';
import { mapFiltersToQueryParams } from '../../../../helpers/filter-mapper';
import { ReservationFilterState } from '../filters/reservation-filter-state.service';
import { Subscription } from 'rxjs';
import { ReservationItemComponent } from "./reservation-item/reservation-item.component";
import { LoadingService } from '../../../../../shared/services/loading.service';
import { NoDataComponent } from "../../../../../shared/ui/no-data/no-data.component";

@Component({
  selector: 'app-reservation-table',
  standalone: true,
  imports: [ReservationItemComponent, NoDataComponent],
  templateUrl: './reservation-table.component.html',
  styleUrl: './reservation-table.component.scss'
})
export class ReservationTableComponent implements OnChanges, OnInit{
  private filterSubscription!: Subscription;

  constructor(
    private service : ReservationService,
    public apiManager : ApiManager<SingleReservationDetailsType[]>,
    private filterState : ReservationFilterState,
    public loadingService : LoadingService
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
    this.apiManager.exeApiRequest(this.service.fetchFilteredReservations(filterUrl))
  }

}
