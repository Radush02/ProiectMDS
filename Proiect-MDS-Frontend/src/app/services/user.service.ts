import { Component, Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, catchError, throwError } from 'rxjs';
import { HttpClientModule } from '@angular/common/http';

import { CookieService } from 'ngx-cookie-service';
import { jwtDecode } from 'jwt-decode';
import { environment } from '../../environments/environments';



@Injectable({
  providedIn: 'root',
})
export class UserService {
  private apiKey = environment.API_KEY;

  constructor(private http: HttpClient, private cookieService: CookieService) {}

  login(info: any): Observable<any> {
    return this.http.post<any>(`${this.apiKey}/user/login`, info);
  }

  register(info: any): Observable<any> {
    console.log(info);
    return this.http.post<any>(`${this.apiKey}/user/register`, info);
  }

  isLoggedIn(): any {
    const token = this.cookieService.get('token');
    if (token) {
      const decodedToken = jwtDecode(token);
      const currentTime = Date.now().valueOf() / 1000;
      if (decodedToken.exp === undefined) return null;
      if (decodedToken.exp < currentTime) return null;

      return JSON.stringify(decodedToken);
    } else {
      return null;
    }
  }
  logout(): Observable<any> {
    this.cookieService.delete('token');
    return this.http.post(`${this.apiKey}/User/Logout`, null);
  }
  getUserDetails(username: string): Observable<any> {
    return this.http.post(
      `${this.apiKey}/User/getUserDetails?username=${username}`,
      null
    );
  }
  getUser(username: string): Observable<any> {
    return this.http.get(`${this.apiKey}/User/getUser?username=${username}`);
  }
  sendConfirmationEmail(info:any):Observable<any>{
    return this.http.post(`${this.apiKey}/User/sendConfirmationEmail`,info);
  }
  uploadPhoto(info:any):Observable<any>{
    return this.http.post(`${this.apiKey}/User/uploadPhoto`,info);
  }
  resetPassword(info:any):Observable<any>{
    return this.http.post(`${this.apiKey}/User/resetPassword`,info);
  }
  forgotPassword(info:any):Observable<any>{
    console.log(info);
    return this.http.post(`${this.apiKey}/User/forgotPassword`,info);
  }


  
  

  
}
