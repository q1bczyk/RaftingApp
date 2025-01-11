import { Component } from '@angular/core';
import { NavigationService } from '../../services/ui/navigation.service';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { NotificationComponent } from "../notification/notification.component";
import { SingleReservationDetailsType } from '../../../shared/types/api/reservation-types/reservation-details.type';
import { NotificationService } from '../../services/ui/notification.service';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [NotificationComponent],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent {
  private notificationSound = new Audio('/sounds/notification.mp3');
  isNotificationOpen: boolean = false;
  newReservations: SingleReservationDetailsType[] = [];

  constructor(
    public navigationService: NavigationService,
    private router: Router,
    private cookiesService: CookieService,
    private notificationService: NotificationService) {
    notificationService.createHubConnection();
    notificationService.newReservationReceived.subscribe((reservation: SingleReservationDetailsType) => {
      this.handleNewReservation(reservation);
    });
  }

  logout(): void {
    this.cookiesService.delete('token', 'admin');
    localStorage.removeItem('role');
    this.router.navigate(['/auth/login']);
  }

  onNotificationClick(): void {
    if (this.isNotificationOpen) this.newReservations = [];
    this.isNotificationOpen = !this.isNotificationOpen;
  }

  private handleNewReservation(reservation: SingleReservationDetailsType): void {
    this.newReservations.push(reservation);
    this.playNotificationSound();
  }

  private playNotificationSound(): void {
    this.notificationSound.play().catch(error => console.error('Error playing sound:', error));
  }
}
