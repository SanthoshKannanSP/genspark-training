import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { CustomerDetails } from "./customer-details/customer-details";
import { ProductCard } from './product-card/product-card';
import { ProductDetails } from './types/ProductDetials';
import { Cart } from "./cart/cart";

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, CustomerDetails, ProductCard, Cart],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected title = 'assignment';
  products : ProductDetails[] = [ 
  {
    id:1,
    name: "Watch",
    price: 4,
    img: "watch.jpg"
  },
  {
    id:2,
    name: "Headphones",
    price: 10,
    img: "headphones.jpg"
  },
  {
    id:3,
    name: "Laptop",
    price: 399,
    img: "laptop.jpg"
  }
  ];

  cartCount:number = 0;
  
}
