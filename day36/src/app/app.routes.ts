import { Routes } from '@angular/router';
import { ProductsPage } from './products-page/products-page';
import { ProductPage } from './product-page/product-page';
import { LoginPage } from './login-page/login-page';
import { ProfilePage } from './profile-page/profile-page';
import { authGuard } from './auth-guard';
import { UserList } from './user-list/user-list';

export const routes: Routes = [
  { path: 'login', component: LoginPage },
  { path: 'userlist', component: UserList },
  { path: 'products', component: ProductsPage, canActivate: [authGuard] },
  { path: 'products/:pid', component: ProductPage, canActivate: [authGuard] },
  { path: 'profile', component: ProfilePage, canActivate: [authGuard] },
];
