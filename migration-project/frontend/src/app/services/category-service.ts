import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Category } from '../models/category';
import { PaginatedResponse } from '../models/paginated-response';

@Injectable({ providedIn: 'root' })
export class CategoryService {
  private baseUrl = 'http://localhost:5288/api/Category';

  constructor(private http: HttpClient) {}

  getCategories() {
    return this.http.get<Category[]>(`${this.baseUrl}/All`);
  }

  getCategoryById(id: number) {
    return this.http.get<Category>(`${this.baseUrl}/${id}`);
  }

  getPagedCategories(page: number) {
    return this.http.get<PaginatedResponse<Category>>(
      `${this.baseUrl}?page=${page}`
    );
  }

  createCategory(category: Category) {
    return this.http.post<Category>(`${this.baseUrl}/Create`, category);
  }

  updateCategory(category: Category) {
    return this.http.post<Category>(`${this.baseUrl}/Edit`, category);
  }

  deleteCategory(id: number) {
    return this.http.delete<boolean>(`${this.baseUrl}/Delete/${id}`);
  }
}
