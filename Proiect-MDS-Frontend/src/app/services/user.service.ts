import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CookieService } from 'ngx-cookie-service';
import { jwtDecode } from 'jwt-decode';
import { environment } from '../../environments/environments';

@Injectable({
    providedIn: 'root',
  })
  
export class UserService {
    private apiKey = environment.API_KEY;
  
    constructor(private http: HttpClient,private cookieService:CookieService) {}
  
    login(info: any): Observable<any> {
      return this.http.post<any>(`${this.apiKey}/user/login`, info);
    }
  
    register(info: any): Observable<any> {
      return this.http.post<any>(`${this.apiKey}/user/register`, info);
    }
  
    isLoggedIn(): any {
      const token=this.cookieService.get('token');
      if(token){
        const decodedToken = jwtDecode(token);
        const currentTime = Date.now().valueOf() / 1000;
        if(decodedToken.exp===undefined)
          return null;
        if(decodedToken.exp<currentTime)
          return null;
        
        return JSON.stringify(decodedToken);
      }
      else{
        return null;
      }
    }
    logout():Observable<any>{
      this.cookieService.delete('token');
      return this.http.post(`${this.apiKey}/User/Logout`,null);
    } 
  }