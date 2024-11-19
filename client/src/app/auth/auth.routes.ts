import { Routes } from '@angular/router';
import { AuthComponent } from './auth.component';

export const authRoutes: Routes = [
    {
        path: '', 
        component: AuthComponent,
        children: [
            {
                path: 'login', 
                loadComponent: () => import('./pages/login/login.component').then(m => m.LoginComponent)
            }
        ]
    },
   
];
