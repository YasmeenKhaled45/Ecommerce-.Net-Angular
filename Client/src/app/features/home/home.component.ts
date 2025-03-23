import { Component, inject } from '@angular/core';
import { BusyService } from '../../Core/Services/busy.service';
import { MatCard } from '@angular/material/card';
import { MatIcon } from '@angular/material/icon';
import { RouterLink } from '@angular/router';
@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    MatCard,
    MatIcon,
    RouterLink
  ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {
  images: string[] = [
    'images/products/blankie.png',
    'images/products/blankie.png',
    'images/products/blankie.png',
    'images/products/blankie.png',
  ];

}
