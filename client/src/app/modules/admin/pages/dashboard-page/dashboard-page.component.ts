import { Component, OnInit } from '@angular/core';
import { PageWrapperComponent } from "../../components/page-wrapper/page-wrapper.component";
import { DashboardService } from '../../../shared/services/api/dashboard.service';
import { ApiManager } from '../../../core/api/api-manager';
import { DashboardDataType } from '../../../shared/types/api/dashboard-types/dashboard-data.type';
import { LoaderComponent } from "../../../shared/ui/loader/loader.component";
import { AvailableEquipmentComponent } from "./components/available-equipment/available-equipment.component";
import { DayliStatsComponent } from "./components/dayli-stats/dayli-stats.component";
import { ReservationComponent } from "../../../client/pages/components/reservation/reservation.component";
import { ReservationsComponent } from "./components/reservations/reservations.component";

@Component({
  selector: 'app-dashboard-page',
  standalone: true,
  imports: [PageWrapperComponent, LoaderComponent, AvailableEquipmentComponent, DayliStatsComponent, ReservationComponent, ReservationsComponent],
  templateUrl: './dashboard-page.component.html',
  styleUrl: './dashboard-page.component.scss'
})
export class DashboardPageComponent implements OnInit{

  constructor(private dashboardService : DashboardService, public apiManager : ApiManager<DashboardDataType>){}

  ngOnInit(): void {
    this.apiManager.exeApiRequest(this.dashboardService.fetchDashboardData(), () => console.log(this.apiManager.data()))
  }

}
