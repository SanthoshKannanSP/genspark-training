import { Component, inject, Input, OnInit } from '@angular/core';
import { ProductModel } from '../models/product';
import { ProudctService } from '../services/product-service';
import { CurrencyPipe } from '@angular/common';

@Component({
  selector: 'app-product',
  imports: [CurrencyPipe],
  templateUrl: './product.html',
  styleUrl: './product.css'
})
export class Product {
  @Input() product!:ProductModel;

}
