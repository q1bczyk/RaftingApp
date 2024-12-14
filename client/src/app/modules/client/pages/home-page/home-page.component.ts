import { Component } from '@angular/core';
import { HeaderComponent } from "../../../admin/components/header/header.component";
import { HomeHeaderComponent } from "../components/home-header/home-header.component";

@Component({
  selector: 'app-home-page',
  standalone: true,
  imports: [HeaderComponent, HomeHeaderComponent],
  templateUrl: './home-page.component.html',
  styleUrl: './home-page.component.scss'
})
export class HomePageComponent {

}
