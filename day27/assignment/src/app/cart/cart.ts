import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-cart',
  imports: [],
  templateUrl: './cart.html',
  styleUrl: './cart.css'
})
export class Cart {
  @Input() itemCount!:number;
}
