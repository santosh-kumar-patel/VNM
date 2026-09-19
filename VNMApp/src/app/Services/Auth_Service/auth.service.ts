import { HttpClient } from '@angular/common/http';
import { Inject, Injectable, PLATFORM_ID } from '@angular/core';
import { LoginRequest } from '../../APIModels/Request/login-request';
import { LoginResponse } from '../../APIModels/Response/login-response';
import { environment } from '../../../environments/environment.development';
import { catchError, map, Observable, of } from 'rxjs';
import { Router } from '@angular/router';
import { isPlatformBrowser } from '@angular/common';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  
  private BaseApiUrl = environment.baseUrl;
  isLoggedIn: boolean = false;
  constructor(private http: HttpClient, private router : Router,@Inject(PLATFORM_ID) private platformId: Object) {}

  login(loginUser:LoginRequest): Observable<boolean> {
    return this.http.post<LoginResponse>(this.BaseApiUrl+"api/Auth/Login",loginUser)
    .pipe(
      map(response => {
        localStorage.setItem('access_token', response.AccessToken);
        return true;
      }),
      catchError(error => {
        console.log(error);
        return of(false);
      })
    );;
  }

  logout(): void {
    localStorage.removeItem('access_token');
  }

  isAuthenticated(): boolean {
    if (isPlatformBrowser(this.platformId)) {
     const token = localStorage.getItem('access_token');
     return !!token && !this.isTokenExpired(token);
   }
   else return false;
  }

   isTokenExpired(token: string): boolean {
    try {
      const payload = JSON.parse(atob(token.split('.')[1]));
      const now = Math.floor(Date.now() / 1000);
      return payload.exp < now;
    } catch (e) {
      return true; // Treat malformed token as expired
    }
  }
   
}
