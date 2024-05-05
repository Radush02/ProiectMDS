import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Form, FormBuilder, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterOutlet, Router, ActivatedRoute } from '@angular/router';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-password-reset',
  standalone: true,
  imports: [CommonModule, RouterOutlet, HttpClientModule, ReactiveFormsModule, FormsModule,HttpClientModule],
  providers: [UserService],
  templateUrl: './password-reset.component.html',
  styleUrl: './password-reset.component.css'
})
export class PasswordResetComponent implements OnInit{
  newPassword: string = '';
  confirmPassword: string = '';
  username: string = '';
  token: string = '';
  
  resetForm: FormGroup;
  constructor(public router: Router,private fb: FormBuilder,private params: ActivatedRoute,private userService: UserService) {
    this.params.queryParams.subscribe(k => {
      this.username = k['username'];
      this.token = k['token'];
    });
    this.resetForm = this.fb.group({
      username: [this.username],
      token: [this.token],
      newPassword: [null],
      confirmPassword: [null]
    });
  }
  resetPassword() {
    if (this.newPassword !== this.confirmPassword) {
      alert('Passwords do not match!');
      return;
    }

    const resetData = {
      username: this.username,
      token: this.token,
      newPassword: this.newPassword
    };
    this.resetForm.value.password = this.newPassword;
    console.log(this.resetForm.value);
    this.userService.resetPassword(this.resetForm.value).subscribe((data) => {
      console.log(data);
      this.router.navigate(['/login']);
    }, (error) => {
      console.log(error);
    });
  }
  ngOnInit() {
    this.resetForm = this.fb.group({
      username: [this.username],
      token: [this.token],
      newPassword: [null],
      confirmPassword: [null]
    });
  }
}
