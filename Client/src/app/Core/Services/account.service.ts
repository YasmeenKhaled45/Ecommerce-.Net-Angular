import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Address, User } from '../../Shared/Models/User';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
baseUrl = environment.apiUrl;
private http = inject(HttpClient);
currentuser = signal<User | null>(null);

login(values:any)
{
  let params = new HttpParams();
  params = params.append('useCookies','true');
  return this.http.post<User>(this.baseUrl + 'login',values,{params,withCredentials:true});
}
signup(values:any){
return this.http.post(this.baseUrl + 'account/register',values);
}

getUserInfo() {
  return this.http.get<User>(this.baseUrl + 'account/user-info', { withCredentials: true })
    .subscribe({
      next: user => this.currentuser.set(user),
      error: err => {
        console.error('Error:', err);
        if (err.status === 401) {
          // Specific handling for 401 (unauthorized)
          console.log('Unauthorized access, maybe re-login or check the token');
        }
      }
    });
}
updateAddress(address:Address){
  return this.http.post(this.baseUrl + 'account/address',address);
}

}
