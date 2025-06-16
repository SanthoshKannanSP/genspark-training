import { Routes } from '@angular/router';
import { ProductsPage } from './products-page/products-page';
import { AboutPage } from './about-page/about-page';

export const routes: Routes = [
    {path: "home", component:ProductsPage},
    {path: "about", component:AboutPage}
];
