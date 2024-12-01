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
                path: 'equipment', 
                loadComponent: () => import('./pages/equipment-page/equipment-page.component').then(m => m.EquipmentPageComponent)
            },
        ]
    },
   
];
