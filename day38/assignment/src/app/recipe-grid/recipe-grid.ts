import { Component, inject, OnInit, signal } from '@angular/core';
import { RecipeModel } from '../models/recipe-model';
import { RecipeService } from '../services/recipe-service';
import { RecipeCard } from '../recipe-card/recipe-card';

@Component({
  selector: 'app-recipe-grid',
  imports: [RecipeCard],
  templateUrl: './recipe-grid.html',
  styleUrl: './recipe-grid.css',
})
export class RecipeGrid implements OnInit {
  recipes = signal<RecipeModel[]>([]);
  private recipeService = inject(RecipeService);

  ngOnInit(): void {
    this.recipeService.getRecipes().subscribe({
      next: (data: any) => this.recipes.set(data.recipes),
      error: (err) => console.log(err),
      complete: () => console.log('Done'),
    });
  }
}
