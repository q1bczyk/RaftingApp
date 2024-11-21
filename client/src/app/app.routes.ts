import { Routes } from '@angular/router';
import { AppComponent } from './app.component';

export const routes: Routes = [
    {
        path: '', 
        component: AppComponent,
    },
    {
        path: 'auth', 
        loadChildren: () => import('./modules/auth/auth.routes').then(m => m.authRoutes)
    },
    {
        path: 'admin', 
        loadChildren: () => import('./modules/admin/admin.routes').then(m => m.adminRoutes)
    }
];
