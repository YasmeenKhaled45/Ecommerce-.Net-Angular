import { inject, Injectable } from '@angular/core';
import { CartService } from './cart.service';
import { Observable, of } from 'rxjs';
import { Cart } from '../../Shared/Models/Cart';

@Injectable({
  providedIn: 'root'
})
export class InitService {
 private cartservice = inject(CartService);
 init() {
  const cartId = localStorage.getItem('cart_id');
  const cart$ = cartId ? this.cartservice.getCart(cartId) : of(null);
  return cart$;
}
}
