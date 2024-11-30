import { Component, Input } from '@angular/core';
import { NavItemType } from '../../types/ui/nav-item.type';
import { navItems } from '../admin-nav/nav-items/nav-item';

@Component({
  selector: 'app-page-wrapper',
  standalone: true,
  imports: [],
  templateUrl: './page-wrapper.component.html',
  styleUrl: './page-wrapper.component.scss'
})
export class PageWrapperComponent {
  @Input() activePageIndex! : number;
  navItems : NavItemType[] = navItems;

 
}
