import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CarService {
  private apiUrl = 'https://localhost:7215/api/Postare'; 

  constructor(private http: HttpClient) { }

  addCar(carData: any): Observable<any> {
    return this.http.post<any>(this.apiUrl, carData);
  }
}
