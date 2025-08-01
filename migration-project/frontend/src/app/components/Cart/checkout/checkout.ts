import { Component } from '@angular/core';
import { CartItem } from '../../../models/cart-item';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { ShoppingCartService } from '../../../services/shopping-cart-service';
import { Router, RouterLink } from '@angular/router';
import { DecimalPipe } from '@angular/common';

@Component({
  selector: 'app-checkout',
  imports: [ReactiveFormsModule, RouterLink, DecimalPipe],
  templateUrl: './checkout.html',
  styleUrl: './checkout.css',
})
export class Checkout {
  cartItems: CartItem[] = [];
  checkoutForm!: FormGroup;

  constructor(
    private shoppingCartService: ShoppingCartService,
    private fb: FormBuilder,
    private router: Router
  ) {}

  ngOnInit() {
    this.cartItems = this.shoppingCartService.getItems();
    this.checkoutForm = this.fb.group({
      customerName: ['', Validators.required],
      customerPhone: ['', Validators.required],
      customerEmail: ['', [Validators.required, Validators.email]],
      customerAddress: ['', Validators.required],
    });
  }

  get total(): number {
    return this.cartItems.reduce(
      (sum, item) => sum + item.quantity * item.product.price,
      0
    );
  }

  processOrder() {
    if (this.checkoutForm.invalid) return;

    const customerInfo = this.checkoutForm.value;

    this.shoppingCartService.processOrder(customerInfo).subscribe({
      next: (response) => {
        this.shoppingCartService.clearCart();
        this.router.navigate(['/checkout/success']);
      },
    });
  }
}
