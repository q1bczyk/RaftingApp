import { Component, Input } from '@angular/core';
import { NavItemType } from '../../types/ui/nav-item.type';
import { getNav } from '../admin-nav/nav-items/nav-item';
import { AuthService } from '../../../auth/services/auth.service';

@Component({
  selector: 'app-page-wrapper',
  standalone: true,
  imports: [],
  templateUrl: './page-wrapper.component.html',
  styleUrl: './page-wrapper.component.scss'
})
export class PageWrapperComponent {
  @Input() activePageIndex! : number;
  navItems : NavItemType[];

  constructor(private authservice : AuthService){
    this.navItems = getNav(authservice.isAdmin());
  }
}
