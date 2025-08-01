import { Injectable } from '@angular/core';
import { PaginatedResponse } from '../models/paginated-response';
import { Product } from '../models/product';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  private baseUrl = 'http://localhost:5288/api/Products';
  constructor(private http: HttpClient) {}

  getPagedProducts(page: number, categoryId?: number) {
    const params: any = { page };
    if (categoryId) params.category = categoryId;
    return this.http.get<PaginatedResponse<Product>>(this.baseUrl, { params });
  }

  getProductById(id: number) {
    return this.http.get<Product>(`${this.baseUrl}/${id}`);
  }
}
