// src/app/components/ticket-list/ticket-list.component.ts
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { SupportService } from '../../services/support.service';
import { CommonModule } from '@angular/common';
import { UserService } from '../../services/user.service';
import { jwtDecode } from 'jwt-decode';
import { CookieService } from 'ngx-cookie-service';
import { tick } from '@angular/core/testing';
import { Router } from '@angular/router';
import { NavbarComponent } from '../navbar/navbar.component';

@Component({
  selector: 'app-ticket-list',
  templateUrl: './ticket-list.component.html',
  standalone: true,
  imports:[
    CommonModule,
    NavbarComponent
  ],
  

})
export class TicketListComponent implements OnInit {
  @Input() tickets: any[] = [];
  
  token=this.cookieService.get('token');
  userid=jwtDecode(this.token);
  user: any;
  constructor(private supportService: SupportService,
    private router: Router,
     private userService: UserService, private cookieService: CookieService) 
  {
    const decodedToken: { [key: string]: any } = jwtDecode(this.token);
    this.user = decodedToken['id'];
  }

  @Output() selectTicket = new EventEmitter<number>();



  ngOnInit() {
    this.loadTickets();
    console.log(this.tickets);
  }

  loadTickets() {
    console.log(this.user);
    this.supportService.getSupportTicketByUserId(this.user).subscribe(response => {
      this.tickets = response;
    });
  }

  openTicket(ticketId: number) {
    console.log(ticketId);
    this.router.navigate(['/ticket'], {
      queryParams: { ticket: ticketId }
    });

  }
}
