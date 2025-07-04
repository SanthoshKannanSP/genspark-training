import { Component, inject, ViewChild } from '@angular/core';
import { ProductCard } from '../product-card/product-card';
import { Product } from '../models/product-model';
import { HttpClient } from '@angular/common/http';
import { PaymentModal } from '../payment-modal/payment-modal';

@Component({
  selector: 'app-products-page',
  imports: [ProductCard, PaymentModal],
  templateUrl: './products-page.html',
  styleUrl: './products-page.css',
})
export class ProductsPage {
  products: Product[] = [];
  http = inject(HttpClient);

  constructor() {
    this.http
      .get<{ products: Product[] }>('https://dummyjson.com/products')
      .subscribe((response: any) => {
        this.products = response.products as Product[];
      });
  }
}
