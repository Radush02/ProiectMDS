import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-confirm-mail',
  standalone: true,
  imports: [],
  providers: [UserService],
  templateUrl: './confirm-mail.component.html',
  styleUrl: './confirm-mail.component.css'
})
export class ConfirmMailComponent implements OnInit {
  username: string = '';
  token: string = '';
  constructor( private params: ActivatedRoute,private userService:UserService) {
    this.params.queryParams.subscribe((params) => {
      this.username = params['username'];
      this.token = encodeURIComponent(params['token']);
    });
  }
  ngOnInit(): void {
    this.userService.confirmEmail(this.username,this.token).subscribe((response)=>{
      const responseElement = document.getElementById('response');
      if(responseElement)
        responseElement.innerText='Email confirmed!';
  }, (error)=>{
    const responseElement = document.getElementById('response');
    if(responseElement)
      responseElement.innerText='Email not confirmed!';
  });
}
}
