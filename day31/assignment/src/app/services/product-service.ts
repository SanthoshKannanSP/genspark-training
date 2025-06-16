import { HttpClient, provideHttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";

@Injectable()
export class ProductService
{
    http = inject(HttpClient);
    
    getProducts(query:string, skip:number=0, limit:number=10)
    {
        return this.http.get(`https://dummyjson.com/products/search?q=${query}&limit=${limit}&skip=${skip}`)
    }
}