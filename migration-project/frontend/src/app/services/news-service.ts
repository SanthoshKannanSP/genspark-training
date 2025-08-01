import { Injectable } from '@angular/core';
import { PaginatedResponse } from '../models/paginated-response';
import { NewsModel } from '../models/newsModel';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class NewsService {
  private baseUrl = 'http://localhost:5288/api/News';
  constructor(private http: HttpClient) {}

  getNews(page: number) {
    return this.http.get<PaginatedResponse<NewsModel>>(
      `${this.baseUrl}?page=${page}`
    );
  }

  getAllNews() {
    return this.http.get<NewsModel[]>(`${this.baseUrl}/All`);
  }

  getNewsById(id: number) {
    return this.http.get<NewsModel>(`${this.baseUrl}/${id}`);
  }

  createNews(news: FormData) {
    return this.http.post(`${this.baseUrl}/Create`, news);
  }

  updateNews(formData: FormData) {
    return this.http.post(`${this.baseUrl}/Update`, formData);
  }

  deleteNews(id: number) {
    return this.http.delete<boolean>(`${this.baseUrl}/Delete/${id}`);
  }

  exportToCSV() {
    window.open(`${this.baseUrl}/Export/Csv`, '_blank');
  }

  exportToExcel() {
    window.open(`${this.baseUrl}/Export/Excel`, '_blank');
  }
}
