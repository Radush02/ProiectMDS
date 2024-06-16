import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { HttpClientModule } from '@angular/common/http';
import { environment } from '../../environments/environments';

@Injectable({
  providedIn: 'root',
})

export class SupportService{
    private apiKey=environment.API_KEY+'/Support';

    constructor(private http: HttpClient) {}
    getSupportTickets(): Observable<any[]> {
        return this.http.get<any>(`${this.apiKey}`);
    }
    getSupportTicketById(id: number): Observable<any> {
        return this.http.get<any>(`${this.apiKey}/SupportBySupportId/${id}`);
    }
    createSupportTicket(ticket: any): Observable<any> {
        return this.http.post<any>(`${this.apiKey}`, ticket);
    }
    getSupportTicketByUserId(userId: number): Observable<any[]> {
        return this.http.get<any>(`${this.apiKey}/SupportByUserId/${userId}`);
    }
    replyToSupportTicket(reply: any): Observable<any> {
        return this.http.post<any>(`${this.apiKey}/reply`, reply);
    }
    sendCreationEmail(email: any): Observable<any> {
        return this.http.post<any>(`${this.apiKey}/CreateEmail`, email);
    }
    sendReplyEmail(email: any): Observable<any> {
        return this.http.post<any>(`${this.apiKey}/ReplyEmail`, email);
    }
}