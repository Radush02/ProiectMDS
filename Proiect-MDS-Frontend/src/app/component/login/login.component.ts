import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import {
  FormBuilder,
  FormGroup,
  Validators,
  ReactiveFormsModule,
} from '@angular/forms';
import { UserService } from '../../services/user.service';
import { CookieService } from 'ngx-cookie-service';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  imports: [CommonModule, RouterOutlet, HttpClientModule, ReactiveFormsModule],
  providers: [UserService],
  styleUrls: ['./login.component.css'],
  standalone: true,
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  errorMessage = '';
  constructor(
    private fb: FormBuilder,
    private httpClient: HttpClient,
    public router: Router,
    private loginService: UserService,
    private cookieService: CookieService
  ) {
    this.loginForm = this.fb.group({
      userName: ['', Validators.required],
      parola: ['', [Validators.required, Validators.minLength(8)]],
      remember: [false, Validators.required],
    });
  }
  login() {
    console.log(this.loginForm.value);

    this.loginService.login(this.loginForm.value).subscribe((response: any) => {
      console.log(response);
      this.cookieService.set(
        'token',
        response.token,
        undefined,
        '/',
        undefined,
        false,
        'Strict'
      );
      this.errorMessage = 'Conectat';
      this.router.navigate(['landingPage']);
      localStorage.setItem('token', response.token);
      console.log(localStorage);
    });
  }
  ngOnInit() {
    if(this.cookieService.get('token') !== ''){
      this.router.navigate(['landingPage']);
    }
    this.loginForm = this.fb.group({
      userName: ['', Validators.required],
      parola: ['', [Validators.required, Validators.minLength(8)]],
      remember: [false, Validators.required],
    });
  }
}
