import { Component, OnInit } from '@angular/core';
import { CategoryService } from '../../../services/category-service';
import { RouterLink } from '@angular/router';
import { Category } from '../../../models/category';
import { PaginatedResponse } from '../../../models/paginated-response';

@Component({
  selector: 'app-category-list',
  imports: [RouterLink],
  templateUrl: './category-list.html',
  styleUrl: './category-list.css',
})
export class CategoryList implements OnInit {
  categories: Category[] = [];

  constructor(private categoryService: CategoryService) {}

  ngOnInit(): void {
    this.categoryService.getCategories().subscribe((data) => {
      this.categories = data;
    });
  }
}
