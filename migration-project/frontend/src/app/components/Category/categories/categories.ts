import { Component, OnInit } from '@angular/core';
import { CategoryService } from '../../../services/category-service';
import { Router, RouterLink } from '@angular/router';

@Component({
  selector: 'app-categories',
  imports: [RouterLink],
  templateUrl: './categories.html',
  styleUrl: './categories.css',
})
export class Categories implements OnInit {
  categories: any[] = [];
  pageNumber: number = 1;
  totalPages: number = 0;

  constructor(
    private categoryService: CategoryService,
    public router: Router
  ) {}

  ngOnInit() {
    this.loadCategories();
  }

  loadCategories() {
    this.categoryService
      .getPagedCategories(this.pageNumber)
      .subscribe((response) => {
        this.categories = response.items;
        this.totalPages = response.totalPage;
      });
  }

  onDelete(id: number) {
    if (confirm('Are you sure you want to delete this?')) {
      this.categoryService.deleteCategory(id).subscribe(() => {
        this.loadCategories();
      });
    }
  }

  goToPage(page: number) {
    this.pageNumber = page;
    this.loadCategories();
  }
}
