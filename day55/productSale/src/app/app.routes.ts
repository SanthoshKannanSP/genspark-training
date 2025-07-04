import { Routes } from '@angular/router';
import { LoginPage } from './login-page/login-page';
import { ProductsPage } from './products-page/products-page';

export const routes: Routes = [
  { path: '', component: LoginPage },
  { path: 'products', component: ProductsPage },
];
