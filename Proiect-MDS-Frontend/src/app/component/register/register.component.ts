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
import {
  validatorParola,
  validatorVarsta,
  validatorPoza,
  validatorEmail,
} from '../../validators/user.validator';
import { MessagePopUpComponent } from '../message-pop-up/message-pop-up.component';
import { MatDialog } from '@angular/material/dialog';

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
    private cookieService: CookieService,
    public dialog: MatDialog
  ) {
    this.registerForm = this.fb.group({
      username: ['', Validators.required],
      parola: [
        '',
        [Validators.required, Validators.minLength(8), validatorParola],
      ],
      confirmareParola: [
        '',
        [Validators.required, Validators.minLength(8), validatorParola],
      ],
      nume: ['', Validators.required],
      prenume: ['', Validators.required],
      email: ['', [Validators.required, validatorEmail]],
      nrTelefon: [
        '',
        [
          Validators.required,
          Validators.pattern(
            '^(?:\\(\\+40\\)\\s?)?0?7\\d{2}\\s?\\d{3}\\s?\\d{3}$'
          ),
        ],
      ],
      dataNasterii: [null, [Validators.required, validatorVarsta]],
      pozaProfil: [null, [Validators.required, validatorPoza]],
      //remember: [false, Validators.required],
    });
  }
  onFileChange(event: any) {
    if (event.target.files.length > 0) {
      this.selectedFile = event.target.files[0];
    }
  }
  register() {
    const formFields = [
      {
        field: 'pozaProfil',
        errorType: 'value',
        errorMessage: 'Poza de profil este obligatorie',
      },
      {
        field: 'nrTelefon',
        errorType: 'nrTelefonInvalid',
        errorMessage: 'Numarul de telefon nu este valid',
      },
      {
        field: 'email',
        errorType: 'emailInvalid',
        errorMessage: 'Emailul nu este valid',
      },
      {
        field: 'parola',
        errorType: 'parolaInvalida',
        errorMessage:
          'Parola trebuie sa contina cel putin o litera mare, o litera mica, o cifra, un caracter special si sa aiba cel putin 8 caractere',
      },
      {
        field: 'dataNasterii',
        errorType: 'minor',
        errorMessage: 'Trebuie sa ai cel putin 18 ani pentru a te inregistra',
      },
      {
        field: 'pozaProfil',
        errorType: 'extensieIncorecta',
        errorMessage:
          'Extensia pozei de profil trebuie sa fie jpg, jpeg sau png',
      },
    ];

    for (let field of formFields) {
      if (
        field.errorType === 'value' &&
        this.registerForm.get(field.field)?.value == null
      ) {
        alert(field.errorMessage);
        return;
      } else if (
        this.registerForm.get(field.field)?.hasError(field.errorType)
      ) {
        alert(field.errorMessage);
        return;
      }
    }
    if (
      this.registerForm.get('parola')?.value !==
      this.registerForm.get('confirmareParola')?.value
    ) {
      alert('Parolele nu coincid');
      return;
    }
    const formData = new FormData();
    formData.append('username', this.registerForm.get('username')?.value);
    formData.append('parola', this.registerForm.get('parola')?.value);
    formData.append('nume', this.registerForm.get('nume')?.value);
    formData.append('prenume', this.registerForm.get('prenume')?.value);
    formData.append('email', this.registerForm.get('email')?.value);
    formData.append('nrTelefon', this.registerForm.get('nrTelefon')?.value);
    formData.append(
      'dataNasterii',
      this.registerForm.get('dataNasterii')?.value
    );
    if (this.selectedFile) {
      formData.append('pozaProfil', this.selectedFile, this.selectedFile.name);
    }
    this.router.navigate(['/login']);
    this.dialog.open(MessagePopUpComponent, {
      data: 'Register confirmation will be sent as soon as we validate your profile picture is appropiate.\n Please verify your email.',
      });
    this.registerService.register(formData).subscribe(
      () => {
      this.registerService.uploadPhoto(formData).subscribe(
        (uploadResult: boolean) => {
        if (uploadResult) {
          this.registerService.sendConfirmationEmail(formData).subscribe(
          () => {
            console.log('email trimis');

          },
          (error) => {
            console.log(error);
          }
          );
        } else {
          console.log('Invalid photo');
        }
        },
        (error) => {
        console.log(error);
        }
      );
      },
      (registerError) => {
      alert(registerError.error.join('\n'));
      }
    );
  }
  ngOnInit() {
    this.registerForm = this.fb.group({
      username: ['', Validators.required],
      parola: [
        '',
        [Validators.required, Validators.minLength(8), validatorParola],
      ],
      confirmareParola: [
        '',
        [Validators.required, Validators.minLength(8), validatorParola],
      ],
      nume: ['', Validators.required],
      prenume: ['', Validators.required],
      email: [
        '',
        [
          Validators.required,
          Validators.pattern('^[a-zA-Z0-9._%+-]+@[a-zA-Z]{3,}.[a-zA-Z]{2,}$'),
        ],
      ],
      nrTelefon: [
        '',
        [
          Validators.required,
          Validators.pattern(
            '^(?:\\(\\+40\\)\\s?)?0?7\\d{2}\\s?\\d{3}\\s?\\d{3}$'
          ),
        ],
      ],
      dataNasterii: [null, [Validators.required, validatorVarsta]],
      pozaProfil: [null, Validators.required],
    });
  }
}
