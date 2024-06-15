import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, forkJoin } from 'rxjs';
import { UserService } from './user.service';

@Injectable({
  providedIn: 'root'
})
export class SupportService {
  private apiUrl = 'https://localhost:7215/api/Support'; 

  constructor(private http: HttpClient, private supportService: SupportService) { }

  AddSupport(supportData: any): Observable<any> {
    return this.http.post<any>(this.apiUrl, supportData)
  }

  clientEmail(supportData: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/clientEmail`, supportData);
  }

  adminEmail(supportData: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/adminEmail`, supportData);
  }
}
