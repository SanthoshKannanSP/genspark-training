import { Routes } from '@angular/router';
import { AddUserPage } from './add-user-page/add-user-page';
import { UserDashboard } from './user-dashboard/user-dashboard';

export const routes: Routes = [
  { path: 'user/add', component: AddUserPage },
  { path: '', component: UserDashboard },
];
