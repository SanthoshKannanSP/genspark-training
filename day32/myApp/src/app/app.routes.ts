import { Routes } from '@angular/router';
import { Products } from './products/products';
import { ProductPage } from './product-page/product-page';
import { Hello } from './hello/hello';
import { Login } from './login/login';
import { Profile } from './profile/profile';
import { AuthGuard } from './auth-gaurd';

export const routes: Routes = [
    {path:"login", component:Login},

    {path: "products", component:Products, children:[
        {path: "hello", component:Hello}
    ]},
    {path:"profile",component:Profile, canActivate:[AuthGuard]}
];
