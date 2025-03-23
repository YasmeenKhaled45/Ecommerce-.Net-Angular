import { Component, inject, Inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from "./layout/header/header/header.component";
import {MatIcon} from '@angular/material/icon';
import {MatButton} from '@angular/material/button';
import {MatBadge} from '@angular/material/badge';
import { HttpClient } from '@angular/common/http';
import { product } from './Shared/Models/product';
import { Pagination } from './Shared/Models/Pagination';
import { ShopserviceService } from './Core/Services/shopservice.service';
import { ShopComponent } from "./features/shop/shop.component";
@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, HeaderComponent, ShopComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'Skint';

}
