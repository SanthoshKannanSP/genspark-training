import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { RecipeGrid } from "./recipe-grid/recipe-grid";

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, RecipeGrid],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected title = 'assignment';
}
