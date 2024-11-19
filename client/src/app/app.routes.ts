import { Routes } from '@angular/router';
import { AppComponent } from './app.component';

export const routes: Routes = [
    {
        path: '', 
        component: AppComponent,
    },
    {
        path: 'auth', 
        loadChildren: () => import('./auth/auth.routes').then(m => m.authRoutes)
    }
];
