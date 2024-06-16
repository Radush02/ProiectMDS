// src/app/components/support-form/support-form.component.ts
import { Component, OnInit } from '@angular/core';
import { SupportService } from '../../services/support.service';
import { FormsModule } from '@angular/forms';
import { jwtDecode } from 'jwt-decode';
import { UserService } from '../../services/user.service';
import { CookieService } from 'ngx-cookie-service';

@Component({
  selector: 'app-support-form',
  imports:[
    FormsModule,

  ],
  templateUrl: './support-form.component.html',
  standalone: true,
  styleUrls: ['./support-form.component.css']
})
export class SupportFormComponent implements OnInit{

  currentUser: any;
  user: any;
  ticket = {
    supportId: 3,
    titlu: '',
    comentariu: '',
    userId: 3
  };

  token=this.cookieService.get('token');
  userid=jwtDecode(this.token);
  username: any;
  constructor(
    private supportService: SupportService,
    private cookieService: CookieService,
    private userService:UserService,

  ) {
    const decodedToken: { [key: string]: any } = jwtDecode(this.token);
    this.currentUser = decodedToken['id'];
    this.ticket.userId = this.currentUser;
  }
  ngOnInit(): void {
    console.log("utilizator curent:");
    console.log(this.currentUser);
  }
  
  


  submitTicket() {
    console.log(this.ticket);
    this.supportService.createSupportTicket(this.ticket).subscribe(response => {
      // Handle response
    });
  }
}
