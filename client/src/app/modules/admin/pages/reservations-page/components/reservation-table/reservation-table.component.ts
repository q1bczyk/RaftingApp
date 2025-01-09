import { Component, Input, OnChanges, OnDestroy, OnInit, SimpleChanges, WritableSignal } from '@angular/core';
import { ReservationService } from '../../../../../shared/services/api/reservation.service';
import { ApiManager } from '../../../../../core/api/api-manager';
import { SingleReservationDetailsType } from '../../../../../shared/types/api/reservation-types/reservation-details.type';
import { mapFiltersToQueryParams } from '../../../../helpers/filter-mapper';
import { ReservationFilterState } from '../filters/reservation-filter-state.service';
import { Subscription } from 'rxjs';
import { ReservationItemComponent } from "./reservation-item/reservation-item.component";
import { LoadingService } from '../../../../../shared/services/loading.service';
import { NoDataComponent } from "../../../../../shared/ui/no-data/no-data.component";
import { ApiSuccessResponse } from '../../../../../core/types/api-success-response.type';
import { ToastService } from '../../../../../shared/services/ui/toasts/toast.service';
import { ConfirmationModalService } from '../../../../../shared/services/confiramtion-modal.service';
import { LoaderComponent } from "../../../../../shared/ui/loader/loader.component";

@Component({
  selector: 'app-reservation-table',
  standalone: true,
  imports: [ReservationItemComponent, NoDataComponent, LoaderComponent],
  templateUrl: './reservation-table.component.html',
  styleUrl: './reservation-table.component.scss'
})
export class ReservationTableComponent implements OnInit, OnDestroy{
  private filtersSubscription: Subscription | null = null;

  constructor(
    private service : ReservationService,
    public apiManager : ApiManager<SingleReservationDetailsType[]>,
    private apiDeleteManager : ApiManager<ApiSuccessResponse>,
    private filterState : ReservationFilterState,
    public loadingService : LoadingService,
    private toastService : ToastService,
    private confirmationModalService : ConfirmationModalService,
    ){
      
  }
  ngOnInit(): void {
    this.fetchData();

    this.filtersSubscription = this.filterState.getFiltersChangedObservable().subscribe(() => {
      this.fetchData();
    });
  }

  ngOnDestroy(): void {
    if (this.filtersSubscription) 
      this.filtersSubscription.unsubscribe();
  }
  
  fetchData() : void{
    const filterUrl : string = mapFiltersToQueryParams(this.filterState.getActiveFilters());
    console.log(filterUrl);
    this.apiManager.exeApiRequest(this.service.fetchFilteredReservations(filterUrl))
  }

  deleteReservation(reservationId : string) : void{
    this.confirmationModalService.openModal(() => this.deleteApiRequest(reservationId));
  }

  private deleteApiRequest(reservationId : string) : void{
    this.apiDeleteManager.exeApiRequest(this.service.delete(reservationId), () => this.onSuccessDelete(reservationId));
  }

  private onSuccessDelete(reservationId : string) : void {
    this.toastService.showToast('Pomyślnie usunięto dane', 'success');
    this.fetchData();
  }

}
