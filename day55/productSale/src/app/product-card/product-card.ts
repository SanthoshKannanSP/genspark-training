import { Component, EventEmitter, inject, Input, Output } from '@angular/core';
import { Product } from '../models/product-model';
import { SlicePipe } from '@angular/common';
import { PaymentService } from '../services/payment-service';

@Component({
  selector: 'app-product-card',
  imports: [],
  templateUrl: './product-card.html',
  styleUrl: './product-card.css',
})
export class ProductCard {
  @Input() product!: Product;
  paymentService = inject(PaymentService);

  buy(): void {
    this.paymentService.setProduct(this.product);
  }
}
