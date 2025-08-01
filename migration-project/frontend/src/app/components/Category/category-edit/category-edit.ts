import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { CategoryService } from '../../../services/category-service';
import { ActivatedRoute, Router } from '@angular/router';
import { Category } from '../../../models/category';

@Component({
  selector: 'app-category-edit',
  imports: [ReactiveFormsModule],
  templateUrl: './category-edit.html',
  styleUrl: './category-edit.css',
})
export class CategoryEdit implements OnInit {
  categoryForm!: FormGroup;
  categoryId!: number;

  constructor(
    private fb: FormBuilder,
    private categoryService: CategoryService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.categoryId = Number(this.route.snapshot.paramMap.get('id'));

    this.categoryForm = this.fb.group({
      name: ['', Validators.required],
    });

    this.categoryService.getCategoryById(this.categoryId).subscribe({
      next: (category) => {
        this.categoryForm.patchValue({
          name: category.name,
        });
      },
      error: () => this.router.navigate(['/categories']),
    });
  }

  onSubmit(): void {
    if (this.categoryForm.valid) {
      const updatedCategory: Category = {
        categoryId: this.categoryId,
        name: this.categoryForm.value.name,
      };

      this.categoryService.updateCategory(updatedCategory).subscribe(() => {
        this.router.navigate(['/categories']);
      });
    }
  }

  onCancel(): void {
    this.router.navigate(['/categories']);
  }
}
