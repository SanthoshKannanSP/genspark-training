import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { ColorService } from '../../../services/color-service';
import { ActivatedRoute, Router } from '@angular/router';
import { Color } from '../../../models/color';

@Component({
  selector: 'app-colors-edit',
  imports: [ReactiveFormsModule],
  templateUrl: './colors-edit.html',
  styleUrl: './colors-edit.css',
})
export class ColorsEdit implements OnInit {
  colorForm!: FormGroup;
  colorId!: number;

  constructor(
    private fb: FormBuilder,
    private colorService: ColorService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.colorId = Number(this.route.snapshot.paramMap.get('id'));

    this.colorForm = this.fb.group({
      name: ['', Validators.required],
    });

    this.colorService.getColorById(this.colorId).subscribe({
      next: (color) => {
        this.colorForm.patchValue({
          name: color.name,
        });
      },
      error: () => this.router.navigate(['/colors']),
    });
  }

  onSubmit(): void {
    if (this.colorForm.valid) {
      const updatedColor: Color = {
        colorId: this.colorId,
        name: this.colorForm.value.name,
      };

      this.colorService.updateColor(updatedColor).subscribe(() => {
        this.router.navigate(['/colors']);
      });
    }
  }

  onCancel(): void {
    this.router.navigate(['/colors']);
  }
}
