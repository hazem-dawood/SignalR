import { Routes } from '@angular/router';
import { LogInComponent } from './core/features/auth/log-in/log-in.component';
import { authGuard } from './core/guards/auth.guard';
import { HomeComponent } from './core/features/chats/home/home.component';

export const routes: Routes = [
    {
        path: 'login',
        component: LogInComponent,
        canDeactivate: [authGuard]
    },
    {
        path: 'home',
        component: HomeComponent,
        canActivate: [authGuard]
    }
];
