import { Component, OnInit } from '@angular/core';
import { MenuModule } from 'primeng/menu'; 
import { NavItemType } from '../../types/ui/nav-item.type';
import { LogoComponent } from "../../../shared/ui/logo/logo.component";
import { CommonModule } from '@angular/common';
import { AuthService } from '../../../auth/services/auth.service';
import { getNav } from './nav-items/nav-item';
import { NavigationService } from '../../services/ui/navigation.service';

@Component({
  selector: 'app-admin-nav',
  templateUrl: './admin-nav.component.html',
  styleUrl: './admin-nav.component.scss',
  standalone: true,
  imports: [MenuModule, LogoComponent, LogoComponent, CommonModule],
})
export class AdminNavComponent implements OnInit {

  links : NavItemType[];

  constructor(private authservice : AuthService, public navigationService : NavigationService){
    this.links = getNav(authservice.isAdmin());
  }

  ngOnInit() {
  }


}
