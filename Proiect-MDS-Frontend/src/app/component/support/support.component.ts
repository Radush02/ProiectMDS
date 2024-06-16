import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterOutlet } from '@angular/router';
import { OpenaiService } from '../../services/openai.service';
import { PostService } from '../../services/post.service';
import { S3Service } from '../../services/s3.service';
import { UserService } from '../../services/user.service';
import { NavbarComponent } from '../navbar/navbar.component';
import { SupportService } from '../../services/support.service';
import { SupportFormComponent } from '../support-form/support-form.component';
import { TicketChatComponent } from '../ticket-chat/ticket-chat.component';
import { TicketListComponent } from '../ticket-list/ticket-list.component';
import { FormsModule } from '@angular/forms';
import { jwtDecode } from 'jwt-decode';
import { CookieService } from 'ngx-cookie-service';


@Component({
  selector: 'app-support',
  standalone: true,
  imports: [
    CommonModule,
    RouterOutlet,
    HttpClientModule,
    ReactiveFormsModule,
    NavbarComponent,
    SupportFormComponent,
    TicketListComponent,
    TicketChatComponent,
    FormsModule
  ],
  providers: [PostService, S3Service, UserService,OpenaiService,SupportService],
  templateUrl: './support.component.html',
  styleUrl: './support.component.css'
})
export class SupportComponent implements OnInit {

  token=this.cookieService.get('token');
  userid=jwtDecode(this.token);
  user: any;
  username: any;
  selectedTicketId: number | null = null;
  tickets: any[] = [];
  constructor(
    private supportService: SupportService,
    private cookieService: CookieService,
    private userService:UserService,

  ) {
    const decodedToken: { [key: string]: any } = jwtDecode(this.token);
    this.user = decodedToken['id'];
    this.username =
        decodedToken[
          'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'
        ];
  }

  

  loadUserTickets(){
    this.supportService.getSupportTicketByUserId(this.user).subscribe((response)=>{
      console.log(response);
    });
  }

  onTicketSelected(ticketId: number) {
    this.selectedTicketId = ticketId;
  }
  getUserInfo() {
    this.userService.getUser(this.username).subscribe((response) => {
      this.user = response;
      console.log(this.username);
    });
  }
  ngOnInit(): void {

      this.getUserInfo();
      this.loadUserTickets();
      console.log(this.username);
      console.log(this.user);
  }


  onSelectTicket(ticketId: number) {
    this.selectedTicketId = ticketId;
  }
}
