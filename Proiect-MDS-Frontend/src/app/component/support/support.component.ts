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

@Component({
  selector: 'app-support',
  standalone: true,
  imports: [
    CommonModule,
    RouterOutlet,
    HttpClientModule,
    ReactiveFormsModule,
    NavbarComponent,
  ],
  providers: [PostService, S3Service, UserService,OpenaiService,SupportService],
  templateUrl: './support.component.html',
  styleUrl: './support.component.css'
})
export class SupportComponent implements OnInit {
  constructor(private postService: PostService,private userService:UserService,
    private s3Service:S3Service,private openaiService:OpenaiService,private supportService:SupportService
  ) {}
  user:any;
  loadUserTickets(){
    this.supportService.getSupportTicketByUserId(this.user.id).subscribe((response)=>{
      console.log(response);
    });
  }
  getUserInfo() {
    this.userService.getUser(this.user).subscribe((response) => {
      this.user = response;
      console.log(this.user);
    });
  }
  ngOnInit(): void {
      this.getUserInfo();
      this.loadUserTickets();
  }
}
