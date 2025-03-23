import { Component, inject, input } from '@angular/core';
import { Items } from '../../Shared/Models/Cart';
import { RouterLink } from '@angular/router';
import { MatIcon } from '@angular/material/icon';
import { MatButton } from '@angular/material/button';
import { CurrencyPipe } from '@angular/common';
import { CartService } from '../../Core/Services/cart.service';

@Component({
  selector: 'app-cart-item',
  standalone: true,
  imports: [
    RouterLink,
    MatIcon,
    MatButton,
    CurrencyPipe
  ],
  templateUrl: './cart-item.component.html',
  styleUrl: './cart-item.component.scss'
})
export class CartItemComponent {
item = input.required<Items>();
cartservice = inject(CartService);

increamentQuantity(){
  this.cartservice.addItemToCart(this.item());
}
decrementQuantity(){
  this.cartservice.removeItemfromCart(this.item().productId);
}
removeItem(){
  this.cartservice.removeItemfromCart(this.item()?.productId,this.item().quantity);
}
}
