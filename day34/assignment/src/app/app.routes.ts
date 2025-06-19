import { Routes } from '@angular/router';
import { AddUserPage } from './add-user-page/add-user-page';
import { FilterPage } from './filter-page/filter-page';

export const routes: Routes = [
  { path: 'add', component: AddUserPage },
  { path: 'filter', component: FilterPage },
];
