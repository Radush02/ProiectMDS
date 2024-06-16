// src/app/components/ticket-chat/ticket-chat.component.ts
import { Component, Input, OnInit } from '@angular/core';
import { SupportService } from '../../services/support.service';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { jwtDecode } from 'jwt-decode';
import { CookieService } from 'ngx-cookie-service';
import { NavbarComponent } from '../navbar/navbar.component';


@Component({
  selector: 'app-ticket-chat',
  imports:[
    FormsModule,
    CommonModule,
    NavbarComponent

  ],
  providers: [SupportService],
  templateUrl: './ticket-chat.component.html',
  standalone: true,
})
export class TicketChatComponent implements OnInit {
  supportId: number=0;
  messages: any[] = [];
  newMessage: string = '';
  currentUser:number;
  ticketSubmitter:number=0;
  constructor(private supportService: SupportService,private query: ActivatedRoute,private cookieService: CookieService) { 
    const token=this.cookieService.get('token');
    this.query.queryParams.subscribe(params => {
      this.supportId = params['ticket'];
    });
    const decodedToken: { [key: string]: any } = jwtDecode(token);
    this.currentUser = decodedToken['id'];

  }

  ngOnInit() {
    this.loadMessages();

  }

  loadMessages() {
    this.supportService.getSupportTicketById(this.supportId).subscribe(response => {
      this.messages = response;
      this.ticketSubmitter=this.messages[0].userId;
      for(let i=0;i<this.messages.length;i++){
        if(this.messages[i].userId==this.ticketSubmitter){
          this.messages[i].comentariu="client: "+this.messages[i].comentariu;
        }
        else{
          this.messages[i].comentariu="admin: "+this.messages[i].comentariu;
        }
      }

    });

  }
  sendMessage() {
    const reply = {
      supportId: this.supportId,
      titlu: this.messages[0].titlu,
      comentariu: this.newMessage,
      userId: this.currentUser
    };
    this.supportService.replyToSupportTicket(reply).subscribe(response => {

      if(this.currentUser!=this.ticketSubmitter){
        console.log({supportId:this.supportId,titlu:this.messages[0].titlu,comentariu:this.newMessage,userId:this.ticketSubmitter});
        this.supportService.sendReplyEmail({supportId:this.supportId,titlu:this.messages[0].titlu,comentariu:this.newMessage,userId:this.ticketSubmitter}).subscribe(response => {
        });
        this.newMessage = '';
      }
      this.loadMessages();
    });
  }
}
