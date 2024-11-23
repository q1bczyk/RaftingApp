import { Component, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { MenuModule } from 'primeng/menu'; 
import { NavItemType } from '../../types/ui/nav-item.type';
import { navItems } from './nav-items/nav-item';

@Component({
  selector: 'app-admin-nav',
  templateUrl: './admin-nav.component.html',
  styleUrl: './admin-nav.component.scss',
  standalone: true,
  imports: [MenuModule],
})
export class AdminNavComponent implements OnInit {

  links : NavItemType[] = navItems; 
 
  constructor() { }

  ngOnInit() {
  }

}
