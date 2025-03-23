import { Component, inject } from '@angular/core';
import {MatIcon} from '@angular/material/icon';
import {MatButton} from '@angular/material/button';
import {MatBadge} from '@angular/material/badge';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { BusyService } from '../../../Core/Services/busy.service';
import {MatProgressBar} from '@angular/material/progress-bar';
import { CartService } from '../../../Core/Services/cart.service';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [
    MatIcon,
    MatButton,
    MatBadge,
    RouterLink,
    RouterLinkActive,
    MatProgressBar
  ],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent {
  busyservice = inject(BusyService);
  cartservice = inject(CartService);
}
