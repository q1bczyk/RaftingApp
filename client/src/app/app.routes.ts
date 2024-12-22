import { Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { AdminGuard } from './modules/admin/guard/admin.guard';
import { HomePageComponent } from './modules/client/pages/home-page/home-page.component';

export const routes: Routes = [
    {
        path: '',
        component: AppComponent,
        children: [
            {
                path: '',
                loadComponent: () => import('./modules/client/pages/home-page/home-page.component').then(m => m.HomePageComponent),
            },
            {
                path: 'reservation/:id',
                loadComponent: () => import('./modules/client/pages/reservation-details-page/reservation-details-page.component').then(m => m.ReservationDetailsPageComponent),
            },
            {
                path: 'auth',
                loadChildren: () => import('./modules/auth/auth.routes').then(m => m.authRoutes),
            },
            {
                path: 'admin',
                loadChildren: () => import('./modules/admin/admin.routes').then(m => m.adminRoutes),
            },
            {
                path: 'not-found',
                loadComponent: () => import('./modules/core/pages/not-found-page/not-found-page.component').then(m => m.NotFoundPageComponent),
            },
            {
                path: 'server-error',
                loadComponent: () => import('./modules/core/pages/server-error-page/server-error-page.component').then(m => m.ServerErrorPageComponent),
            },
        ],
    }
];
