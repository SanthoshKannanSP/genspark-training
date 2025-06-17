import { Component, HostListener, OnInit } from '@angular/core';
import { ProudctService } from '../services/product-service';
import { ProductModel } from '../models/product';
import { NgIf } from '@angular/common';
import { Product } from "../product/product";
import { FormsModule } from '@angular/forms';
import { debounce, debounceTime, Subject, switchMap, tap } from 'rxjs';
import { Router, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-products',
  imports: [Product, FormsModule, RouterOutlet],
  templateUrl: './products.html',
  styleUrl: './products.css'
})
export class Products implements OnInit {
  products:ProductModel[] = [];
  cartCount:number = 0;
  queryData:string = "";
  querySubject = new Subject<string>();
  loading = false;
  limit = 10;
  skip = 0;
  total = 0;
  constructor(private productService:ProudctService)
  {

  }

  handleSearch()
  {
    this.querySubject.next(this.queryData);
  }

  ngOnInit(): void {
    this.querySubject.pipe(
      tap(()=>this.loading = true),
      debounceTime(5000),
      switchMap(query => this.productService.getProductSearchResult(query,this.skip,this.limit)),
      tap(()=>this.loading = false)
      ).subscribe({
        next: (data:any) => {this.products = data.products as ProductModel[]; this.total =data.total},
      })
  };

  increaseCartCount(pid:number)
  {
    this.cartCount += 1
  }

  @HostListener('window:scroll',[])
  onScroll():void
  {

    const scrollPosition = window.innerHeight + window.scrollY;
    const threshold = document.body.offsetHeight-100;
    if(scrollPosition>=threshold && this.products?.length<this.total)
    {
      console.log(scrollPosition);
      console.log(threshold)
      
      this.loadMore();
    }
  }
  loadMore(){
    this.loading = true;
    this.skip += this.limit;
    this.productService.getProductSearchResult(this.queryData,this.limit,this.skip)
        .subscribe({
          next:(data:any)=>{
            [...this.products,...data.products]
            this.loading = false;
          }
        })
  }
}
