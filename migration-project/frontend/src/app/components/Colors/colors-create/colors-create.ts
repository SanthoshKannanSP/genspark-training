import { Component } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { ColorService } from '../../../services/color-service';
import { Router } from '@angular/router';
import { Color } from '../../../models/color';

@Component({
  selector: 'app-colors-create',
  imports: [ReactiveFormsModule],
  templateUrl: './colors-create.html',
  styleUrl: './colors-create.css',
})
export class ColorsCreate {
  colorForm!: FormGroup;

  constructor(
    private fb: FormBuilder,
    private colorService: ColorService,
    private router: Router
  ) {
    this.colorForm = this.fb.group({
      name: ['', Validators.required],
    });
  }

  onSubmit(): void {
    if (this.colorForm.valid) {
      const newColor: Color = {
        colorId: null,
        name: this.colorForm.value.name,
      };

      this.colorService.createColor(newColor).subscribe(() => {
        this.router.navigate(['/colors']);
      });
    }
  }

  onCancel(): void {
    this.router.navigate(['/colors']);
  }
}
