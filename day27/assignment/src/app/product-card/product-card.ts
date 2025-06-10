import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ProductDetails } from '../types/ProductDetials';

@Component({
  selector: 'app-product-card',
  imports: [],
  templateUrl: './product-card.html',
  styleUrl: './product-card.css'
})
export class ProductCard {
  @Input() product! : ProductDetails;
  @Input() cartCount! : number;
  @Output() cartCountChange = new EventEmitter<number>();

  count:number = 0;

  increaseCount()
  {
    this.count +=1;
    this.cartCount += 1;
    this.cartCountChange.emit(this.cartCount);
  }

  decreaseCount()
  {
    if (this.count>0)
    {
      this.count -= 1;
      this.cartCount -= 1;
      this.cartCountChange.emit(this.cartCount);
    }
  }
}