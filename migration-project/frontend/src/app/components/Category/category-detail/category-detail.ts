import { Component, OnInit } from '@angular/core';
import { Category } from '../../../models/category';
import { ActivatedRoute, Router } from '@angular/router';
import { CategoryService } from '../../../services/category-service';

@Component({
  selector: 'app-category-detail',
  imports: [],
  templateUrl: './category-detail.html',
  styleUrl: './category-detail.css',
})
export class CategoryDetail implements OnInit {
  category: Category | null = null;

  constructor(
    private route: ActivatedRoute,
    private categoryService: CategoryService,
    private router: Router
  ) {}

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.categoryService.getCategoryById(id).subscribe({
      next: (data) => (this.category = data),
      error: () => this.router.navigate(['/categories']),
    });
  }

  goToEdit(): void {
    if (this.category) {
      this.router.navigate(['/categories/edit', this.category.categoryId]);
    }
  }

  goBack(): void {
    this.router.navigate(['/categories']);
  }
}
