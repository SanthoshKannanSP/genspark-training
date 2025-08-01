import { Component, OnInit } from '@angular/core';
import { Order } from '../../../models/order';
import { ShoppingCartService } from '../../../services/shopping-cart-service';
import { DatePipe } from '@angular/common';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-orders',
  imports: [DatePipe, RouterLink],
  templateUrl: './orders.html',
  styleUrl: './orders.css',
})
export class Orders implements OnInit {
  orders: Order[] = [];
  totalPages = 0;
  pageNumber = 1;
  pageSize = 10;

  constructor(private shoppingCartService: ShoppingCartService) {}

  ngOnInit() {
    this.loadOrders();
  }

  loadOrders() {
    this.shoppingCartService.getOrders(this.pageNumber).subscribe((res) => {
      this.orders = res.items;
      this.totalPages = res.totalPage;
    });
  }

  onPageChange(page: number) {
    this.pageNumber = page;
    this.loadOrders();
  }

  goToPage(page: number) {
    this.pageNumber = page;
    this.loadOrders();
  }

  exportOrders() {
    this.shoppingCartService.exportOrderListing().subscribe((blob) => {
      const url = window.URL.createObjectURL(blob as Blob);
      window.open(url, '_blank');
    });
  }
}
