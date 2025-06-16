import { Component, HostListener, inject } from '@angular/core';
import { ProductModel } from '../models/product-model';
import { ProductService } from '../services/product-service';
import { debounceTime, distinctUntilChanged, Subject, switchMap, tap } from 'rxjs';
import { FormsModule } from '@angular/forms';
import { ProductCard } from '../product-card/product-card';

@Component({
  selector: 'app-products-page',
  imports: [FormsModule, ProductCard],
  templateUrl: './products-page.html',
  styleUrl: './products-page.css'
})
export class ProductsPage {
  products:ProductModel[] = [];
  productService = inject(ProductService);
  searchString:string="";
  searchSubject = new Subject<string>();
  loading:boolean = false;
  limit=30;
  skip=0;
  total =0;

  handleSearchProducts(){
    this.searchSubject.next(this.searchString);
  }
  
  ngOnInit(): void {
    this.searchSubject.pipe(
      distinctUntilChanged(),
      tap(()=> {this.loading=true; this.skip=0}),
      debounceTime(400),
      switchMap(query=>this.productService.getProducts(query,this.skip,this.limit)),
       tap(()=>this.loading=false)).subscribe({
        next:(data:any)=>{
                    this.products = data.products as ProductModel[];
                    this.total = data.total;
                    console.log(this.total)
                    }
      });

  }
  
  @HostListener('window:scroll',[])
  onScroll():void
  {
    const scrollPosition = window.innerHeight + window.scrollY;
    const threshold = document.body.offsetHeight-100;
    if(scrollPosition>=threshold && this.products?.length<this.total)
    {
      this.loadMore();
    }
  }

  loadMore(){
    this.skip += this.limit;
    this.productService.getProducts(this.searchString,this.skip,this.limit)
        .subscribe({
          next:(data:any)=>{
            this.products.push(...data.products);
          }
        })
  }
}
