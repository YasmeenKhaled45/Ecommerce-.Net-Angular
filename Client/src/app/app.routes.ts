import { Routes } from '@angular/router';
import { HomeComponent } from './features/home/home.component';
import { ShopComponent } from './features/shop/shop.component';
import { ProductDetailsComponent } from './features/shop/product-details/product-details.component';
import { NotFoundComponent } from './Shared/not-found/not-found.component';
import { ServerErrorComponent } from './Shared/server-error/server-error.component';
import { CartComponent } from './features/cart/cart.component';
import { CheckOutComponent } from './features/check-out/check-out.component';
import { LoginComponent } from './features/login/login.component';
import { SignupComponent } from './features/signup/signup.component';

export const routes: Routes = [
    {path:'',component:HomeComponent},
    {path:'shop',component:ShopComponent},
    {path:'shop/:id',component:ProductDetailsComponent},
    {path:'cart',component:CartComponent},
    {path:'checkout',component:CheckOutComponent},
    {path:'account/login',component:LoginComponent},
    {path:'account/Signup',component:SignupComponent},
    {path:'not-found',component:NotFoundComponent},
    {path:'server-error',component:ServerErrorComponent},
    {path:'**', redirectTo:'not-found',pathMatch:'full'}
];
