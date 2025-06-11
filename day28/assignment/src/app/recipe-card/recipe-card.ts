import { Component, Input } from '@angular/core';
import { RecipeModel } from '../models/recipe-model';

@Component({
  selector: 'app-recipe-card',
  imports: [],
  templateUrl: './recipe-card.html',
  styleUrl: './recipe-card.css'
})
export class RecipeCard {
  @Input() recipe:RecipeModel|null = null;
  cardClicked = false;
  cardStyle = "card"

  onClick()
  {
    if (this.cardClicked)
    {
      this.cardClicked = false;
      this.cardStyle = "card";
    }
    else
    {
      this.cardClicked = true;
      this.cardStyle = "card flip"
    }
  }
}
