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
    },
    {
        path: 'not-found', 
        loadComponent: () => import('./modules/core/pages/not-found-page/not-found-page.component').then(m => m.NotFoundPageComponent)
    },
    {
        path: 'server-error', 
        loadComponent: () => import('./modules/core/pages/server-error-page/server-error-page.component').then(m => m.ServerErrorPageComponent)
    },
];
