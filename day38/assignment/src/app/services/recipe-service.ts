import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class RecipeService {
  private http = inject(HttpClient);
  getRecipes() {
    return this.http.get('https://dummyjson.com/recipes');
  }
}
