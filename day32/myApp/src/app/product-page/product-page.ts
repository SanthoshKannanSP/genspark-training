import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-product-page',
  imports: [],
  templateUrl: './product-page.html',
  styleUrl: './product-page.css'
})
export class ProductPage implements OnInit {
  pname:string = "";
  router = inject(ActivatedRoute)

  ngOnInit(): void {
    this.pname = this.router.snapshot.params["pn"];
  }
}
