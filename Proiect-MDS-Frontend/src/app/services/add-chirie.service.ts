import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, forkJoin } from 'rxjs';
import { UserService } from './user.service';

@Injectable({
  providedIn: 'root'
})
export class ChirieService {
  private apiUrl = 'https://localhost:7215/api/Chirie'; 

  constructor(private http: HttpClient, private userService: UserService) { }

  addChirie(chirieData: any): Observable<any> {
    return this.http.post<any>(this.apiUrl, chirieData)
  }

  rentEmailConfirmation(chirieData: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/rentConfirmationEmail`, chirieData);
  }
}
