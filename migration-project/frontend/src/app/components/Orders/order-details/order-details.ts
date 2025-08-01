import { Component, OnInit } from '@angular/core';
import { Order } from '../../../models/order';
import { OrderDetail } from '../../../models/order-details';
import { DatePipe, DecimalPipe } from '@angular/common';
import { ShoppingCartService } from '../../../services/shopping-cart-service';
import { ActivatedRoute, RouterLink } from '@angular/router';

@Component({
  selector: 'app-order-details',
  imports: [DecimalPipe, DatePipe, RouterLink],
  templateUrl: './order-details.html',
  styleUrl: './order-details.css',
})
export class OrderDetails implements OnInit {
  orderId!: number;
  order!: Order;
  orderDetails!: OrderDetail[];

  constructor(
    private shoppingCartService: ShoppingCartService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.orderId = Number(this.route.snapshot.paramMap.get('id'));
    this.loadOrder();
  }

  loadOrder() {
    this.shoppingCartService.getOrder(this.orderId).subscribe({
      next: (response) => (this.order = response),
    });
    this.shoppingCartService.getOrderDetails(this.orderId).subscribe({
      next: (response) => (this.orderDetails = response),
    });
  }
}
