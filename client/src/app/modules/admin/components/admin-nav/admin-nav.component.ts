import { Component, OnInit } from '@angular/core';
import { MenuModule } from 'primeng/menu'; 
import { NavItemType } from '../../types/ui/nav-item.type';
import { navItems } from './nav-items/nav-item';
import { LogoComponent } from "../../../shared/ui/logo/logo.component";
import { NavigationService } from '../../services/ui/navigation.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-admin-nav',
  templateUrl: './admin-nav.component.html',
  styleUrl: './admin-nav.component.scss',
  standalone: true,
  imports: [MenuModule, LogoComponent, LogoComponent, CommonModule],
})
export class AdminNavComponent implements OnInit {

  links : NavItemType[] = navItems; 
 
  constructor(public navigationService : NavigationService) { 

  }

  ngOnInit() {
  }


}
