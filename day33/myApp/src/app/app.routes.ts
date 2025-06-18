import { Routes } from '@angular/router';
import { ProductsPage } from './products-page/products-page';
import { ProductPage } from './product-page/product-page';
import { LoginPage } from './login-page/login-page';
import { ProfilePage } from './profile-page/profile-page';
import { authGuard } from './auth-guard';

export const routes: Routes = [
  { path: 'login', component: LoginPage },
  { path: 'products', component: ProductsPage, canActivate: [authGuard] },
  { path: 'products/:pid', component: ProductPage, canActivate: [authGuard] },
  { path: 'profile', component: ProfilePage, canActivate: [authGuard] },
];
