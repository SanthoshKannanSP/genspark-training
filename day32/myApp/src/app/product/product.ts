import { Component, EventEmitter, inject, Input, OnInit, Output } from '@angular/core';
import { ProductModel } from '../models/product';
import { ProudctService } from '../services/product-service';
import { CurrencyPipe } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-product',
  imports: [CurrencyPipe],
  templateUrl: './product.html',
  styleUrl: './product.css'
})
export class Product {
  @Input() product!:ProductModel;
  @Output() addToCart: EventEmitter<number> = new EventEmitter<number>();
  router = inject(Router);

  handleClick()
  {
    this.addToCart.emit(this.product.pid);
    this.router.navigateByUrl("/"+this.product.title);
  }
}
