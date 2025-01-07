import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HomeHeaderComponent } from "./pages/components/home-header/home-header.component";

@Component({
  selector: 'app-client',
  standalone: true,
  imports: [RouterOutlet, HomeHeaderComponent],
  templateUrl: './client.component.html',
  styleUrl: './client.component.scss'
})
export class ClientComponent {

}
