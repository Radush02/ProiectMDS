import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, ActivatedRoute, RouterOutlet } from '@angular/router';
import { UserService } from '../../services/user.service';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-forgot-password',
  standalone: true,
  imports: [CommonModule, RouterOutlet, HttpClientModule, ReactiveFormsModule, FormsModule,HttpClientModule],
  providers: [UserService],
  templateUrl: './forgot-password.component.html',
  styleUrl: './forgot-password.component.css'
})
export class ForgotPasswordComponent implements OnInit{
  forgotPasswordForm: FormGroup;
  constructor(public router: Router,private fb: FormBuilder,private params: ActivatedRoute,private userService: UserService) {
    this.forgotPasswordForm = this.fb.group({
      Email: [null, Validators.required],
      Username: [null, Validators.required]
    });
  }

  sendEmail() {

    console.log(this.forgotPasswordForm.value);
    this.userService.forgotPassword(this.forgotPasswordForm.value).subscribe((data) => {
      console.log(data);
      this.router.navigate(['/login']);
    }, (error) => {
      console.log(error);
    });
  }
  ngOnInit() {
    this.forgotPasswordForm = this.fb.group({
      Email: [null, Validators.required],
      Username: [null, Validators.required]
    });
  }
}
