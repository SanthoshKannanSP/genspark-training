import { Component, OnInit } from '@angular/core';
import { ProudctService } from '../services/product-service';
import { ProductModel } from '../models/product';
import { NgIf } from '@angular/common';
import { Product } from "../product/product";

@Component({
  selector: 'app-products',
  imports: [Product],
  templateUrl: './products.html',
  styleUrl: './products.css'
})
export class Products implements OnInit {
  products:ProductModel[]|null = null;
  cartCount:number = 0;
  constructor(private productService:ProudctService)
  {

  }
  ngOnInit(): void {
    this.productService.getAllProduct().subscribe({
      next: (data:any)=> {this.products = data.products as ProductModel[];console.log(this.products)},
      error: (err) => {console.log(err)},
      complete: () => {console.log("Done")}
    })
  };

  increaseCartCount(pid:number)
  {
    this.cartCount += 1
  }

}
