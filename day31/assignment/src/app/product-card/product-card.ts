import { Component, Input } from '@angular/core';
import { ProductModel } from '../models/product-model';
import { HighlightPipe } from '../miscs/highlight-pipe';


@Component({
  selector: 'app-product-card',
  imports: [HighlightPipe],
  templateUrl: './product-card.html',
  styleUrl: './product-card.css'
})
export class ProductCard {
  @Input() details!:ProductModel;
  @Input() searchTerm:string = "";
}
