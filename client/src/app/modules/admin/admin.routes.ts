import { Routes } from '@angular/router';
import { AdminComponent } from './admin.component';
import { AuthGuard } from './guard/auth.guard';
import { AdminGuard } from './guard/admin.guard';

export const adminRoutes: Routes = [
    {
        path: '', 
        component: AdminComponent,
        canActivate : [AuthGuard],
        children: [
            {
                path: 'dashboard', 
                loadComponent: () => import('./pages/dashboard-page/dashboard-page.component').then(m => m.DashboardPageComponent)
            },
            {
                path: 'bookings', 
                loadComponent: () => import('./pages/reservations-page/reservations-page.component').then(m => m.ReservationsPageComponent)
            }, 
            {
                path: 'equipment', 
                canActivate: [AdminGuard],
                loadComponent: () => import('./pages/equipment-page/equipment-page.component').then(m => m.EquipmentPageComponent),
            }, 
            {
                path: 'users', 
                canActivate: [AdminGuard],
                loadComponent: () => import('./pages/accounts-page/accounts-page.component').then(m => m.AccountsPageComponent),
            },
            {
                path: 'settings', 
                canActivate: [AdminGuard],
                loadComponent: () => import('./pages/system-settings-page/system-settings-page.component').then(m => m.SystemSettingsPageComponent),
            },
        ]
    },
   
];
