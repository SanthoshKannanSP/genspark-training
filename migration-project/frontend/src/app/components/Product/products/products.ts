import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { Product } from '../../../models/product';
import { PaginatedResponse } from '../../../models/paginated-response';
import { DecimalPipe } from '@angular/common';
import { ProductService } from '../../../services/product-service';
import { ShoppingCartService } from '../../../services/shopping-cart-service';
import { CartProduct } from '../../../models/cart-item';

@Component({
  selector: 'app-products',
  imports: [RouterLink, DecimalPipe],
  templateUrl: './products.html',
  styleUrl: './products.css',
})
export class Products implements OnInit {
  products: Product[] = [];
  pageNumber: number = 1;
  categoryId?: number;
  totalPages: number = 0;
  baseUrl = 'http://localhost:5288/api/Products/Image/';

  constructor(
    private productService: ProductService,
    private shoppingCartService: ShoppingCartService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.route.queryParams.subscribe((params) => {
      this.pageNumber = params['page'] || 1;
      this.categoryId = params['category'];
      this.loadProducts();
    });
  }

  loadProducts(): void {
    this.productService
      .getPagedProducts(this.pageNumber, this.categoryId)
      .subscribe((response) => {
        this.products = response.items;
        this.totalPages = response.totalPage;
      });
  }

  goToPage(page: number): void {
    this.router.navigate([], {
      relativeTo: this.route,
      queryParams: {
        page: page,
        category: this.categoryId,
      },
      queryParamsHandling: 'merge',
    });
  }

  addToCart(product: Product) {
    this.shoppingCartService.addToCart(product);
  }
}
