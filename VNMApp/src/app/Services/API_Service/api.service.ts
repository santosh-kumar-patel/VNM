import { Injectable } from '@angular/core';
import{HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import{ environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class APIService {

  private BaseApiUrl = environment.baseUrl;
  
  constructor(private http: HttpClient) {}
  
// private getAuthHeaders(): HttpHeaders {
//    const token = localStorage.getItem('access_token');
//    return new HttpHeaders({
//     'Content-Type': 'application/json',
//     ...(token ? { Authorization: `Bearer ${token}` } : {})
//    });
// }

  // POST
  post<T>(path: string, data: any): Observable<T> {
    return this.http.post<T>(`${this.BaseApiUrl}${path}`, data);
  }

  // GET
  get<T>(path: string, params?: HttpParams): Observable<T> {
    return this.http.get<T>(`${this.BaseApiUrl}${path}`, { params });
  }

  // GET by ID
  getById<T>(path: string, id: number | string): Observable<T> {
    const encodedId = encodeURIComponent(id);
    return this.http.get<T>(`${this.BaseApiUrl}${path}/${encodedId}`);
  }

  // DELETE
  delete<T>(path: string, id: number | string): Observable<T> {
    const encodedId = encodeURIComponent(id);
    return this.http.delete<T>(`${this.BaseApiUrl}${path}/${encodedId}`);
  }

}
