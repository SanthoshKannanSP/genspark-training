import { Component, EventEmitter, inject, Input, Output } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { Product } from '../models/product-model';
import { PaymentService } from '../services/payment-service';

@Component({
  selector: 'app-payment-modal',
  imports: [ReactiveFormsModule, FormsModule],
  templateUrl: './payment-modal.html',
  styleUrl: './payment-modal.css',
})
export class PaymentModal {
  product!: Product;
  paymentDetail: any = null;
  paymentService = inject(PaymentService);

  paymentForm: FormGroup;

  constructor(private fb: FormBuilder) {
    this.paymentForm = this.fb.group({
      customerName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      contact: [
        '',
        [
          Validators.required,
          Validators.minLength(10),
          Validators.maxLength(10),
        ],
      ],
    });
    this.paymentService.product$.subscribe({
      next: (product) => (this.product = product),
    });
    this.paymentService.paymentStatus$.subscribe({
      next: (data) => (this.paymentDetail = data),
    });
  }

  onSubmit(): void {
    if (this.paymentForm.valid) {
      this.paymentService.pay(
        this.product.title,
        this.product.price,
        this.paymentForm.value
      );
    } else {
      this.paymentForm.markAllAsTouched();
    }
  }

  onClose(): void {
    // this.close.emit();
  }
}
