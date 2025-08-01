import { Component, OnInit } from '@angular/core';
import { Product } from '../../../models/product';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { ProductService } from '../../../services/product-service';
import { DecimalPipe } from '@angular/common';
import { ShoppingCartService } from '../../../services/shopping-cart-service';

@Component({
  selector: 'app-product-details',
  imports: [RouterLink, DecimalPipe],
  templateUrl: './product-details.html',
  styleUrl: './product-details.css',
})
export class ProductDetails implements OnInit {
  product!: Product;
  baseUrl = 'http://localhost:5288/api/Products/Image/';

  constructor(
    private route: ActivatedRoute,
    private productService: ProductService,
    private shoppingCartService: ShoppingCartService
  ) {}

  ngOnInit(): void {
    const id = Number.parseInt(this.route.snapshot.paramMap.get('id')!);
    this.productService.getProductById(id).subscribe((product) => {
      this.product = product;
    });
  }

  addToCart() {
    this.shoppingCartService.addToCart(this.product);
  }
}
