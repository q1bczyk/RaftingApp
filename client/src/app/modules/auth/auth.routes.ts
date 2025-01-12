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
            },
            {
                path: 'password-reset', 
                loadComponent: () => import('./pages/password-reset/password-reset.component').then(m => m.PasswordResetComponent)
            },
            {
                path: 'new-password', 
                loadComponent: () => import('./pages/new-password/new-password.component').then(m => m.NewPasswordComponent)
            },
            {
                path: 'confirm-account', 
                loadComponent: () => import('./pages/confrim-account/confrim-account.component').then(m => m.ConfrimAccountComponent)
            },
        ]
    },
   
];
