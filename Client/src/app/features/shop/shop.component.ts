import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { ShopserviceService } from '../../Core/Services/shopservice.service';
import { product } from '../../Shared/Models/product';
import {MatCard} from '@angular/material/card';
import {MatDialog} from '@angular/material/dialog';
import { ProductItemComponent } from "../product-item/product-item.component";
import { FilterDialogComponent } from './filter-dialog/filter-dialog.component';
import { MatButton } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon';
import {MatMenu, MatMenuTrigger} from '@angular/material/menu';
import { MatListOption, MatSelectionList, MatSelectionListChange } from '@angular/material/list';
import { FormsModule } from '@angular/forms';
import { ShopParams } from '../../Shared/Models/ShopParams';
import {MatPaginator, PageEvent} from '@angular/material/paginator'
import { Pagination } from '../../Shared/Models/Pagination';
@Component({
  selector: 'app-shop',
  standalone: true,
  imports: [
    MatCard,
    ProductItemComponent,
    MatButton,
    MatIcon,
    MatMenu,
    MatSelectionList,
    MatListOption,
    MatMenuTrigger,
    FormsModule,
   MatPaginator
],
  templateUrl: './shop.component.html',
  styleUrl: './shop.component.scss'
})
export class ShopComponent implements OnInit{
  private shopservice = inject(ShopserviceService);
  private dialogService = inject(MatDialog);
    products?: Pagination<product>;
    sortOptions = [
      {name: 'A-Z' , value: 'name'},
      {name: 'PRICE: LOW-HIGH' , value: 'priceAsc'},
      {name: 'PRICE: HIGH-LOW' , value: 'priceDesc'},
      {name: 'BEST RATED' , value: 'TopRated'}
    ]
    shopParams = new ShopParams();
    pagesizeOptions = [5,10,15,20];
    searchSuggestions: product[] = [];
    constructor(private http:HttpClient){
  
    }
    ngOnInit(): void {
      this.intializeShope();
    }
   intializeShope(){
    this.shopservice.getBrands();
    this.shopservice.getTypes();
    this.getProducts();
   }

   getProducts(){
    this.shopservice.getProducts(this.shopParams).subscribe({
      next: response=> this.products = response,
      error: error=> console.log(error)
    })
   }
    
   onSearchChange() {
    this.shopParams.pageNumber = 1;
    this.getProducts();  // Trigger product fetch
    this.fetchSearchSuggestions();  // Fetch search suggestions
  }

  fetchSearchSuggestions() {
    if (this.shopParams.search && this.shopParams.search.length > 0) {
      // Set search in shopParams and get products
      this.shopParams.pageNumber = 1;  // Ensure the search suggestions reset the page
      this.shopservice.getProducts(this.shopParams).subscribe({
        next: response => {
          // You can update the searchSuggestions from the response data
          this.searchSuggestions = response.data; // Assuming 'items' is the product list
        },
        error: error => console.log(error)
      });
    } else {
      this.searchSuggestions = [];  // Clear suggestions if search is empty
    }
  }
  clearSearch(): void {
    this.shopParams.search = '';
    this.searchSuggestions = []; // Clear suggestions
    // Optionally, redirect to the shop page or reset filters:
    // this.router.navigate(['/shop']);
  }
  selectSuggestion(suggestion: product) {
    this.shopParams.search = suggestion.name;
    this.fetchSearchSuggestions();  
    this.getProducts();
    this.searchSuggestions = [];
  }
  
   onSortChange(event:MatSelectionListChange){
    const selectedOption = event.options[0];
    if(selectedOption){
       this.shopParams.sort = selectedOption.value;
       this.shopParams.pageNumber = 1;
       this.getProducts();

    }
   }
   handlePageEvent(event:PageEvent){
      this.shopParams.pageNumber = event.pageIndex + 1;
      this.shopParams.pageSize = event.pageSize;
      this.getProducts();
   }
   openFiltersDialog(){
    const dialogref = this.dialogService.open(FilterDialogComponent,{
      minWidth: '500px',
      data:{
        selectedBrands : this.shopParams.brand,
        selectedTypes : this.shopParams.types
      }
    });
    dialogref.afterClosed().subscribe({
      next: result=>{
        if(result){
          this.shopParams.brand = result.selectedBrands,
          this.shopParams.types = result.selectedTypes,
          this.shopParams.pageNumber = 1;
          this.shopservice.getProducts(this.shopParams).subscribe({
            next: response=> this.products = response,
            error: error=> console.log(error)
          })
        }
      }
    })
   }
}
