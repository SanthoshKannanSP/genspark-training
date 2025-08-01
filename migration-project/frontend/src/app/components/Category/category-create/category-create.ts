import { Component } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { CategoryService } from '../../../services/category-service';
import { Router } from '@angular/router';
import { Category } from '../../../models/category';

@Component({
  selector: 'app-category-create',
  imports: [ReactiveFormsModule],
  templateUrl: './category-create.html',
  styleUrl: './category-create.css',
})
export class CategoryCreate {
  categoryForm!: FormGroup;

  constructor(
    private fb: FormBuilder,
    private categoryService: CategoryService,
    private router: Router
  ) {
    this.categoryForm = this.fb.group({
      name: ['', Validators.required],
    });
  }

  onSubmit(): void {
    if (this.categoryForm.valid) {
      const newCategory: Category = {
        categoryId: null,
        name: this.categoryForm.value.name,
      };

      this.categoryService.createCategory(newCategory).subscribe(() => {
        this.router.navigate(['/categories']);
      });
    }
  }

  onCancel(): void {
    this.router.navigate(['/categories']);
  }
}
