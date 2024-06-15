import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { HttpClientModule } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ChirieService {
  private apiUrl = 'https://localhost:7215/api/Chirie';

  constructor(private http: HttpClient) {}

  addChirie(chirie: any): Observable<any> {
    return this.http.post<any>(this.apiUrl, chirie);
  }

  deleteChirie(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }

  updateChirie(id: number, chirie: any): Observable<any> {
    return this.http.put<any>(`${this.apiUrl}/${id}`, chirie);
  }

  getChirieByDateRange(dataStart: string, dataStop: string): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/data/${dataStart}/${dataStop}`);
  }
}
