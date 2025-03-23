import { Component, inject, Input } from '@angular/core';
import { product } from '../../Shared/Models/product';
import { MatCard, MatCardActions, MatCardContent } from '@angular/material/card';
import { CommonModule, CurrencyPipe } from '@angular/common';
import { MatButton } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon';
import { RouterLink } from '@angular/router';
import { CartService } from '../../Core/Services/cart.service';

@Component({
  selector: 'app-product-item',
  standalone: true,
  imports: [
    MatCard,
    MatCardContent,
    CurrencyPipe,
    MatCardActions,
    MatButton,
    MatIcon,
    CommonModule,
    RouterLink
  ],
  templateUrl: './product-item.component.html',
  styleUrl: './product-item.component.scss'
})
export class ProductItemComponent {
@Input() product?:product;

 cartservice = inject(CartService);
}