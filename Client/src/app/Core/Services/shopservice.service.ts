import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Inject, Injectable } from '@angular/core';
import { product } from '../../Shared/Models/product';
import { Observable } from 'rxjs';
import { ShopParams } from '../../Shared/Models/ShopParams';
import { Pagination } from '../../Shared/Models/Pagination';

@Injectable({
  providedIn: 'root'
})
export class ShopserviceService {
  baseUrl = 'https://localhost:5001/api/'
  brands: string[] = [];
  types:string[] = [];
  private http = inject(HttpClient);
  
  getProducts(shopParams:ShopParams) {
    let params = new HttpParams();
    if(shopParams.brand.length > 0){
      params = params.append('brands',shopParams.brand.join(','));
    }
    if(shopParams.types.length > 0){
      params = params.append('types',shopParams.types.join(','));
    }
    if(shopParams.sort){
      params = params.append('sort',shopParams.sort);
    }
    if(shopParams.search){
      params = params.append('search',shopParams.search);
    }
    params = params.append('pageSize',shopParams.pageSize);
    params = params.append('pageIndex',shopParams.pageNumber);
    return this.http.get<Pagination<product>>(this.baseUrl + 'product',{params});
  }
  getProduct(id:number){
    return this.http.get<product>(this.baseUrl + 'product/'+id);
  }
  getTypes(){
    if(this.types.length > 0) return;
    return this.http.get<string[]>(this.baseUrl + 'product/types').subscribe({
      next: response=> this.types = response
    })
  }
  getBrands(){
    if(this.brands.length > 0) return;
    return this.http.get<string[]>(this.baseUrl + 'product/brands').subscribe({
      next:response=> this.brands = response
    })
  }
}
