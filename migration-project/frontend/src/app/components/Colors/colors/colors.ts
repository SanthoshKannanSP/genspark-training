import { Component, OnInit } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { ColorService } from '../../../services/color-service';
import { Color } from '../../../models/color';

@Component({
  selector: 'app-colors',
  imports: [RouterLink],
  templateUrl: './colors.html',
  styleUrl: './colors.css',
})
export class Colors implements OnInit {
  colors!: Color[];
  constructor(public router: Router, private colorService: ColorService) {}

  ngOnInit() {
    this.loadColors();
  }

  onDelete(id: number) {
    if (confirm('Are you sure you want to delete this?')) {
      this.colorService.deleteColor(id).subscribe(() => {
        this.loadColors();
      });
    }
  }

  loadColors() {
    this.colorService.getColors().subscribe((response) => {
      this.colors = response;
    });
  }
}
