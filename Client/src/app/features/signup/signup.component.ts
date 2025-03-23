import { Component, inject } from '@angular/core';
import { FormArrayName, FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButton } from '@angular/material/button';
import { MatCard } from '@angular/material/card';
import { MatError, MatFormField, MatLabel } from '@angular/material/form-field';
import { MatIcon } from '@angular/material/icon';
import { MatInput } from '@angular/material/input';
import { AccountService } from '../../Core/Services/account.service';
import { Router } from '@angular/router';
import { SnackBarService } from '../../Core/Services/snack-bar.service';

@Component({
  selector: 'app-signup',
  standalone: true,
  imports: [
    ReactiveFormsModule, MatCard ,MatButton , MatError,
     MatFormField , MatLabel , MatInput
  ],
  templateUrl: './signup.component.html',
  styleUrl: './signup.component.scss'
})
export class SignupComponent {
private fb = inject(FormBuilder);
private accservice = inject(AccountService);
private router = inject(Router);
private snackbar = inject(SnackBarService);
validationErrors?: string [] ;
registerForm = this.fb.group({
  firstName: ['', [Validators.required, Validators.minLength(3)]],
  lastName: ['', [Validators.required, Validators.minLength(3)]],
  email: ['', [Validators.required, Validators.email]],
  password: ['', [Validators.required, Validators.minLength(6)]]
});

OnSubmit(){
this.accservice.signup(this.registerForm.value).subscribe({
  next : () =>{
    this.snackbar.success();
    this.router.navigateByUrl('/account/login');
  },
  error: (errors) => {
    this.validationErrors = errors;
  }
});
}
}
