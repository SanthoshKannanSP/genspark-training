import { Component } from '@angular/core';
import { CartItem } from '../../../models/cart-item';
import { ShoppingCartService } from '../../../services/shopping-cart-service';
import { RouterLink } from '@angular/router';
import { DecimalPipe } from '@angular/common';

@Component({
  selector: 'app-cart',
  imports: [RouterLink, DecimalPipe],
  templateUrl: './cart.html',
  styleUrl: './cart.css',
})
export class Cart {
  cart: CartItem[] = [];

  constructor(public shoppingCartService: ShoppingCartService) {}

  ngOnInit(): void {
    this.cart = this.shoppingCartService.getItems();
  }

  updateQuantity(item: CartItem, event: Event) {
    var quantity = (event.target as HTMLInputElement).value;
    const qty = parseInt(quantity, 10);
    if (!isNaN(qty) && qty > 0) {
      this.shoppingCartService.updateQuantity(item.product.productId, qty);
    }
  }

  removeItem(productId: number) {
    this.shoppingCartService.removeFromCart(productId);
    this.cart = this.shoppingCartService.getItems();
  }

  getTotal(): number {
    return this.shoppingCartService.getTotal();
  }
}
