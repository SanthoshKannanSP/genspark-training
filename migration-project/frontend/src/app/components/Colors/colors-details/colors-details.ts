import { Component, OnInit } from '@angular/core';
import { Color } from '../../../models/color';
import { ActivatedRoute, Router } from '@angular/router';
import { ColorService } from '../../../services/color-service';

@Component({
  selector: 'app-colors-details',
  imports: [],
  templateUrl: './colors-details.html',
  styleUrl: './colors-details.css',
})
export class ColorsDetails implements OnInit {
  color: Color | null = null;

  constructor(
    private route: ActivatedRoute,
    private colorService: ColorService,
    private router: Router
  ) {}

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.colorService.getColorById(id).subscribe({
      next: (data) => (this.color = data),
      error: () => this.router.navigate(['/colors']),
    });
  }

  goToEdit(): void {
    if (this.color) {
      this.router.navigate(['/colors/edit', this.color.colorId]);
    }
  }

  goBack(): void {
    this.router.navigate(['/colors']);
  }
}
