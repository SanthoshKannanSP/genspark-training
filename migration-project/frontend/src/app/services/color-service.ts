import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Color } from '../models/color';
import { PaginatedResponse } from '../models/paginated-response';

@Injectable({
  providedIn: 'root',
})
export class ColorService {
  private baseUrl = 'http://localhost:5288/api/Color';

  constructor(private http: HttpClient) {}

  getColors() {
    return this.http.get<Color[]>(`${this.baseUrl}`);
  }

  getColorById(id: number) {
    return this.http.get<Color>(`${this.baseUrl}/${id}`);
  }

  createColor(color: Color) {
    return this.http.post<Color>(`${this.baseUrl}/Create`, color);
  }

  updateColor(color: Color) {
    return this.http.post<Color>(`${this.baseUrl}/Edit`, color);
  }

  deleteColor(id: number) {
    return this.http.delete<boolean>(`${this.baseUrl}/Delete/${id}`);
  }
}
