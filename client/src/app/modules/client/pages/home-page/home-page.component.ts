import { Component } from '@angular/core';
import { HeaderComponent } from "../../../admin/components/header/header.component";
import { HomeHeaderComponent } from "../components/home-header/home-header.component";
import { ReservationComponent } from "../components/reservation/reservation.component";

@Component({
  selector: 'app-home-page',
  standalone: true,
  imports: [HeaderComponent, HomeHeaderComponent, ReservationComponent],
  templateUrl: './home-page.component.html',
  styleUrl: './home-page.component.scss'
})
export class HomePageComponent {

}
