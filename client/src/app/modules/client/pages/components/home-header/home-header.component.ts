import { Component } from '@angular/core';
import { LogoComponent } from "../../../../shared/ui/logo/logo.component";

@Component({
  selector: 'app-home-header',
  standalone: true,
  imports: [LogoComponent],
  templateUrl: './home-header.component.html',
  styleUrl: './home-header.component.scss'
})
export class HomeHeaderComponent {

}
