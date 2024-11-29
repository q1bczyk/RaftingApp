import { Routes } from '@angular/router';
import { AdminComponent } from './admin.component';

export const adminRoutes: Routes = [
    {
        path: '', 
        component: AdminComponent,
        children: [
            {
                path: 'equipment', 
                loadComponent: () => import('./pages/equipment-page/equipment-page.component').then(m => m.EquipmentPageComponent)
            },
        ]
    },
   
];
