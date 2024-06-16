// src/app/components/ticket-chat/ticket-chat.component.ts
import { Component, Input, OnInit } from '@angular/core';
import { SupportService } from '../../services/support.service';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';


@Component({
  selector: 'app-ticket-chat',
  imports:[
    BrowserModule,
    FormsModule,
    HttpClientModule,
    CommonModule
  

  ],
  templateUrl: './ticket-chat.component.html',
  standalone: true,
})
export class TicketChatComponent implements OnInit {
  @Input() supportId: number = 0;
  messages: any[] = [];
  newMessage: string = '';
  currentUser = { id: 6 }; // Exemplu de utilizator curent

  constructor(private supportService: SupportService) { }

  ngOnInit() {
    this.loadMessages();
    setInterval(() => this.loadMessages(), 5000); // Polling every 5 seconds
  }

  loadMessages() {
    this.supportService.getSupportTicketById(this.supportId).subscribe(response => {
      this.messages = response;
    });
  }

  sendMessage() {
    const reply = {
      supportId: this.supportId,
      titlu: 'Re: ' + this.messages[0].titlu,
      comentariu: this.newMessage,
      userId: this.currentUser.id
    };

    this.supportService.replyToSupportTicket(reply).subscribe(response => {
      this.newMessage = '';
      this.loadMessages();
    });
  }
}
