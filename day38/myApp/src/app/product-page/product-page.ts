import { Component, inject, OnInit } from '@angular/core';
import { ProductModel } from '../models/product-model';
import { ProductService } from '../services/product-service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-product-page',
  imports: [],
  templateUrl: './product-page.html',
  styleUrl: './product-page.css',
})
export class ProductPage implements OnInit {
  product: ProductModel | null = null;
  productService = inject(ProductService);
  router = inject(ActivatedRoute);

  ngOnInit(): void {
    const pid = this.router.snapshot.params['pid'];
    this.productService.getProductDetail(pid).subscribe({
      next: (data) => (this.product = data as ProductModel),
    });
  }
}
