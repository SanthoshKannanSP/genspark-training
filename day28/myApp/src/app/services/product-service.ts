import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";

@Injectable()
export class ProudctService
{
    private http = inject(HttpClient);
    getProduct(pid:number)
    {
        return this.http.get('https://dummyjson.com/products/'+pid);
    }

    getAllProduct()
    {
        return this.http.get('https://dummyjson.com/products');
    }
}