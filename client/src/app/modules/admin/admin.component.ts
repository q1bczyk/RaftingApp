import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { AdminNavComponent } from './components/admin-nav/admin-nav.component';
import { HeaderComponent } from "./components/header/header.component";
import { NavigationService } from './services/ui/navigation.service';
import { CommonModule } from '@angular/common';
import { FormModalComponent } from "./components/form-modal/form-modal.component";

@Component({
  selector: 'app-admin',
  standalone: true,
  imports: [RouterOutlet, AdminNavComponent, HeaderComponent, CommonModule],
  templateUrl: './admin.component.html',
  styleUrl: './admin.component.scss'
})
export class AdminComponent {
  constructor(public navigationService : NavigationService){}

  
}
