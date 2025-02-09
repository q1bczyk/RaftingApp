import { Routes } from '@angular/router';
import { ClientComponent } from './client.component';
import { ReservationResolver } from './resolvers/reservation.resolver';

export const clientRoutes: Routes = [
    {
        path: '',
        component: ClientComponent,
        children: [
            {
                path: '',
                loadComponent: () => import('./pages/home-page/home-page.component').then(m => m.HomePageComponent),
            },
            {
                path: 'reservation/:id',
                loadComponent: () => import('./pages/reservation-details-page/reservation-details-page.component').then(m => m.ReservationDetailsPageComponent),
                resolve: {reservationDetails : ReservationResolver}
            },
        ],
    }
];
