import { Component, inject, OnInit } from '@angular/core';
import { ShopserviceService } from '../../../Core/Services/shopservice.service';
import { ActivatedRoute } from '@angular/router';
import { product } from '../../../Shared/Models/product';
import { CurrencyPipe } from '@angular/common';
import { MatIcon } from '@angular/material/icon';
import { MatButton } from '@angular/material/button';
import { MatFormField, MatLabel } from '@angular/material/form-field';
import {MatInput} from '@angular/material/input';
import { MatDivider } from '@angular/material/divider';
import { CartService } from '../../../Core/Services/cart.service';
import { FormsModule } from '@angular/forms';
@Component({
  selector: 'app-product-details',
  standalone: true,
  imports: [
    CurrencyPipe,
    MatIcon,
    MatButton,
    MatFormField,
    MatInput,
    MatLabel,
    MatDivider,
    FormsModule
  ],
  templateUrl: './product-details.component.html',
  styleUrl: './product-details.component.scss'
})
export class ProductDetailsComponent implements OnInit{

private shopservice = inject(ShopserviceService);
private activatedRoute = inject(ActivatedRoute);
Product?:product;
quantityInCarts = 0;
quantity = 1;
private cartservice = inject(CartService);

ngOnInit(): void {
  this.loadProduct();
}
loadProduct(){
  const id = this.activatedRoute.snapshot.paramMap.get('id');
  if(!id) return;
  this.shopservice.getProduct(+id).subscribe({
    next: Product=> {
      this.Product = Product;
      this.updateQuantityInCart();
    },
    
    error: error=>console.log(error)
  })
}

updateCart(){
  if(!this.Product) return;
  if(this.quantity > this.quantityInCarts){
    const itemAdded = this.quantity - this.quantityInCarts;
    this.quantityInCarts += itemAdded;
    this.cartservice.addItemToCart(this.Product,itemAdded);

  }else{
    const itemremoved = this.quantityInCarts - this.quantity;
    this.quantityInCarts -= itemremoved;
    this.cartservice.removeItemfromCart(this.Product.id,itemremoved);
  }
}

updateQuantityInCart(){
  this.quantityInCarts = this.cartservice.cart()?.items
  .find(x=>x.productId === this.Product?.id)?.quantity || 0;
  this.quantity = this.quantityInCarts || 1;
}
getButtonText(){
  return this.quantityInCarts > 0 ? 'Update cart' : 'Add to cart'
}
}
