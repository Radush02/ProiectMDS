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
  selectedFile: File | null = null;
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
      dataNasterii: [null, Validators.required],
      pozaProfil: [null, Validators.required]
    });
  }
  onFileChange(event: any) {
    if (event.target.files.length > 0) {
      this.selectedFile = event.target.files[0];
    }
  }
  register() {
    const formData = new FormData();
    formData.append('username', this.registerForm.get('username')?.value);
    formData.append('parola', this.registerForm.get('parola')?.value);
    formData.append('nume', this.registerForm.get('nume')?.value);
    formData.append('prenume', this.registerForm.get('prenume')?.value);
    formData.append('email', this.registerForm.get('email')?.value);
    formData.append('nrTelefon', this.registerForm.get('nrTelefon')?.value);
    formData.append('dataNasterii', this.registerForm.get('dataNasterii')?.value);
    //let fileInput = this.registerForm.get('pozaProfil');
    if (this.selectedFile) {
      formData.append('pozaProfil', this.selectedFile, this.selectedFile.name);
    }
  

    this.registerService
      .register(formData)
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
      dataNasterii: [null, Validators.required],
      pozaProfil: [null, Validators.required]
      //remember: [false, Validators.required],
    });
  }
  // register() {
  //   alert('de implementat');
  // }
}
