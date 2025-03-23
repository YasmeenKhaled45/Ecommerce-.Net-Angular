import { Component, inject } from '@angular/core';
import { MatButton } from '@angular/material/button';
import { MatFormField, MatLabel } from '@angular/material/form-field';
import { MatIcon } from '@angular/material/icon';
import { RouterLink } from '@angular/router';
import { CartService } from '../../Core/Services/cart.service';
import { CurrencyPipe } from '@angular/common';

@Component({
  selector: 'app-order-summary',
  standalone: true,
  imports: [
    RouterLink,
    MatButton,
    MatIcon,
    MatFormField,
    MatLabel,
    CurrencyPipe
  ],
  templateUrl: './order-summary.component.html',
  styleUrl: './order-summary.component.scss'
})
export class OrderSummaryComponent {
cartservice = inject(CartService);
}
