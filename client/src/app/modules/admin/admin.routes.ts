import { Routes } from '@angular/router';
import { AdminComponent } from './admin.component';
import { AdminGuard } from './guard/admin.guard';

export const adminRoutes: Routes = [
    {
        path: '', 
        component: AdminComponent,
        canActivate : [AdminGuard],
        children: [
            {
                path: 'bookings', 
                loadComponent: () => import('./pages/reservations-page/reservations-page.component').then(m => m.ReservationsPageComponent)
            }, 
            {
                path: 'equipment', 
                loadComponent: () => import('./pages/equipment-page/equipment-page.component').then(m => m.EquipmentPageComponent)
            }, 
            {
                path: 'settings', 
                loadComponent: () => import('./pages/system-settings-page/system-settings-page.component').then(m => m.SystemSettingsPageComponent)
            },
        ]
    },
   
];
