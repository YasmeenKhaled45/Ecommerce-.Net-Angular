import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { MatButton } from '@angular/material/button';
import { MatCard } from '@angular/material/card';
import { MatFormField, MatLabel } from '@angular/material/form-field';
import { MatInput } from '@angular/material/input';
import { AccountService } from '../../Core/Services/account.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    MatCard,
    MatFormField,
    MatInput,
    MatButton,
    MatLabel
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
private fb = inject(FormBuilder);
private accountservice = inject(AccountService);
private router = inject(Router);

loginform = this.fb.group({
  email:[''],
  password:['']
});

OnSubmit(){
  this.accountservice.login(this.loginform.value).subscribe({
    next: () => {
      this.accountservice.getUserInfo();
      this.router.navigateByUrl('/shop');
    }
  })
}
}
