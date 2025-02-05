import { Routes } from '@angular/router';
import { USER_ROUTES } from './components/user/user.routes';

export const routes: Routes = [
    { path: 'user', children: USER_ROUTES },
    { path: '**', redirectTo: 'user' }
  ];
