import { Component, Injectable, NgModule } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environments';


@Injectable({
  providedIn: 'root'
})
export class CardService {
  private apiUrl = environment.API_KEY + '/Card';

  constructor(private http: HttpClient) {}

  getCards(id:number): Observable<any[]> {
    return this.http.get<any>(this.apiUrl+"/"+id);
  }

  addCard(card: any): Observable<any> {
    return this.http.post<any>(this.apiUrl, card);
  }

  deleteCard(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }

  updateCard(id: number, card: any): Observable<any> {
    return this.http.put<any>(`${this.apiUrl}/${id}`, card);
  }
}
