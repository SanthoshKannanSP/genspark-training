import { Injectable } from '@angular/core';
import { CartItem, CartProduct } from '../models/cart-item';
import { Product } from '../models/product';
import { CustomerInfo } from '../models/customer-info';
import { HttpClient } from '@angular/common/http';
import { PaginatedResponse } from '../models/paginated-response';
import { Order } from '../models/order';
import { OrderDetail } from '../models/order-details';

const CART_KEY = 'shopping_cart';

@Injectable({
  providedIn: 'root',
})
export class ShoppingCartService {
  private baseUrl = 'http://localhost:5288/api/Order';
  private cart: CartItem[] = [];

  constructor(private http: HttpClient) {
    this.loadCart();
  }

  private loadCart() {
    const storedCart = localStorage.getItem(CART_KEY);
    this.cart = storedCart ? JSON.parse(storedCart) : [];
  }

  private saveCart() {
    localStorage.setItem(CART_KEY, JSON.stringify(this.cart));
  }

  getItems() {
    return [...this.cart];
  }

  addToCart(product: Product) {
    var cartProduct: CartProduct = {
      productId: product.productId,
      productName: product.productName,
      price: product.price,
    };
    const index = this.cart.findIndex(
      (item) => item.product.productId === cartProduct.productId
    );

    if (index === -1) {
      this.cart.push({ product: cartProduct, quantity: 1 });
    } else {
      this.cart[index].quantity += 1;
    }

    this.saveCart();
  }

  removeFromCart(productId: number) {
    const index = this.cart.findIndex(
      (item) => item.product.productId === productId
    );
    if (index !== -1) {
      this.cart.splice(index, 1);
      this.saveCart();
    }
  }

  clearCart() {
    this.cart = [];
    this.saveCart();
  }

  updateQuantity(productId: number, quantity: number) {
    const item = this.cart.find((i) => i.product.productId === productId);
    if (item && quantity > 0) {
      item.quantity = quantity;
      this.saveCart();
    }
  }

  getTotal() {
    return this.cart.reduce(
      (sum, item) => sum + item.product.price * item.quantity,
      0
    );
  }

  processOrder(customerInfo: CustomerInfo) {
    const orderRequest = {
      ...customerInfo,
      cartItems: this.cart.map((item) => ({
        productId: item.product.productId,
        quantity: item.quantity,
        price: item.product.price,
      })),
    };

    console.log(orderRequest);

    return this.http.post(`${this.baseUrl}/Create`, orderRequest);
  }

  getOrders(page: number) {
    return this.http.get<PaginatedResponse<Order>>(
      `${this.baseUrl}?page=${page}`
    );
  }

  getOrder(id: number) {
    return this.http.get<Order>(`${this.baseUrl}/${id}`);
  }

  getOrderDetails(id: number) {
    return this.http.get<OrderDetail[]>(`${this.baseUrl}/Details/${id}`);
  }

  exportOrderListing() {
    return this.http.get(`${this.baseUrl}/ExportPdf`, {
      responseType: 'blob',
    });
  }
}
