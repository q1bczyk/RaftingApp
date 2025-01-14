import { Component, OnInit } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { AdminNavComponent } from './components/admin-nav/admin-nav.component';
import { HeaderComponent } from "./components/header/header.component";
import { NavigationService } from './services/ui/navigation.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-admin',
  standalone: true,
  imports: [RouterOutlet, AdminNavComponent, HeaderComponent, CommonModule],
  templateUrl: './admin.component.html',
  styleUrl: './admin.component.scss'
})
export class AdminComponent implements OnInit{
  constructor(public navigationService : NavigationService, private router : Router){}

  ngOnInit(): void {
    this.router.navigate(['/admin/dashboard']);
  }

}
