import { Component } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { CategoryList } from './components/Category/category-list/category-list';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, CategoryList, RouterLink],
  templateUrl: './app.html',
  styleUrl: './app.css',
})
export class App {
  currentYear = new Date().getFullYear();
}
