import { Component } from '@angular/core';
import { NavigationService } from '../../services/ui/navigation.service';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent {
  constructor(
    public navigationService : NavigationService,
    private router : Router,
    private cookiesService : CookieService){}

  logout() : void{
    this.cookiesService.delete('token', 'admin');
    this.router.navigate(['/auth/login']);
  }
}
