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
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, RouterOutlet, HttpClientModule, ReactiveFormsModule],
  providers: [UserService],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css',
})
export class RegisterComponent implements OnInit {
  registerForm!: FormGroup;
  errorMessage = '';
  constructor(
    private fb: FormBuilder,
    private httpClient: HttpClient,
    public router: Router,
    private registerService: UserService,
    private cookieService: CookieService
  ) {
    this.registerForm = this.fb.group({
      username: ['', Validators.required],
      parola: ['', [Validators.required, Validators.minLength(8)]],
      nume: ['', Validators.required],
      prenume: ['', Validators.required],
      email: ['', Validators.required],
      nrTelefon: ['', [Validators.required, Validators.maxLength(15)]],
    });
  }
  register() {
    console.log(this.registerForm.value);

    this.registerService
      .register(this.registerForm.value)
      .subscribe((response: any) => {
        console.log(response);
        this.registerService
          .login(this.registerForm.value)
          .subscribe((loginResponse) => {
            this.cookieService.set(
              'token',
              loginResponse.token,
              undefined,
              '/',
              undefined,
              false,
              'Strict'
            );
            this.errorMessage = 'Conectat';
            this.router.navigate(['landingPage']);
          });
      });
  }
  ngOnInit() {
    this.registerForm = this.fb.group({
      username: ['', Validators.required],
      parola: ['', [Validators.required, Validators.minLength(8)]],
      nume: ['', Validators.required],
      prenume: ['', Validators.required],
      email: ['', Validators.required],

      nrTelefon: ['', [Validators.required, Validators.maxLength(15)]],
      //remember: [false, Validators.required],
    });
  }
  // register() {
  //   alert('de implementat');
  // }
}
