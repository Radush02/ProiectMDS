import { Component, Inject } from '@angular/core';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import {
  MAT_DIALOG_DATA,
  MatDialogClose,
  MatDialogRef,
  MatDialogContent,
  MatDialogActions,
  MatDialogTitle,
} from '@angular/material/dialog';
import { MatInputModule } from '@angular/material/input';
import { Profile } from '../../Profile';
import { Observable } from 'rxjs';
import { HttpClientModule } from '@angular/common/http';
import { HttpClient } from '@angular/common/http';
import { Validators } from '@angular/forms';
import { FormBuilder } from '@angular/forms';
import {
  validatorParola,
  validatorVarsta,
  validatorPoza,
} from '../../validators/user.validator';
import { UserService } from '../../services/user.service';
import { CookieService } from 'ngx-cookie-service';
import { FormGroup } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Input } from '@angular/core';

@Component({
  selector: 'app-edit-profile',
  standalone: true,
  imports: [
    CommonModule,
    HttpClientModule,
    ReactiveFormsModule,
    FormsModule,
    MatDialogActions,
    MatDialogContent,
    MatDialogClose,
    MatButtonModule,
    MatInputModule,
    MatDialogTitle,
    HttpClientModule,
  ],
  providers: [UserService, CookieService],
  templateUrl: './edit-profile.component.html',
  styleUrl: './edit-profile.component.css',
})
export class EditProfileComponent {
  selectedFile: File | null = null;
  editForm!: FormGroup;
  errorMessage = '';
  @Input() profile?: Profile;

  constructor(
    private fb: FormBuilder,
    private httpClient: HttpClient,
    public router: Router,
    private editService: UserService,
    private cookieService: CookieService
  ) {
    this.editForm = this.fb.group({
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
      pozaProfil: [
        this.profile?.linkPozaProfil,
        [Validators.required, validatorPoza],
      ],
      //remember: [false, Validators.required],
    });
  }

  save() {
    const formFields = [
      {
        field: 'pozaProfil',
        errorType: 'value',
        errorMessage: 'Poza de profil este obligatorie',
      },
      {
        field: 'nrTelefon',
        errorType: 'pattern',
        errorMessage: 'Numarul de telefon nu este valid',
      },
      {
        field: 'email',
        errorType: 'pattern',
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
        this.editForm.get(field.field)?.value == null
      ) {
        alert(field.errorMessage);
        return;
      } else if (this.editForm.get(field.field)?.hasError(field.errorType)) {
        alert(field.errorMessage);
        return;
      }
    }
    if (
      this.editForm.get('parola')?.value !==
      this.editForm.get('confirmareParola')?.value
    ) {
      alert('Parolele nu coincid');
      return;
    }
    const formData = new FormData();
    formData.append('username', this.editForm.get('username')?.value);
    formData.append('parola', this.editForm.get('parola')?.value);
    formData.append('nume', this.editForm.get('nume')?.value);
    formData.append('prenume', this.editForm.get('prenume')?.value);
    formData.append('email', this.editForm.get('email')?.value);
    formData.append('nrTelefon', this.editForm.get('nrTelefon')?.value);
    formData.append('dataNasterii', this.editForm.get('dataNasterii')?.value);
    //let fileInput = this.editForm.get('pozaProfil');
    if (this.selectedFile) {
      formData.append('pozaProfil', this.selectedFile, this.selectedFile.name);
    }

    this.profile = {
      username: this.editForm.get('username')?.value,
      nume: this.editForm.get('nume')?.value,
      prenume: this.editForm.get('prenume')?.value,
      email: this.editForm.get('email')?.value,
      nrTelefon: this.editForm.get('nrTelefon')?.value,
      dataNasterii: this.editForm.get('dataNasterii')?.value,
      parola: this.editForm.get('parola')?.value,
      linkPozaProfil: this.selectedFile?.name,
    };
    //send data
    this.router.navigate(['profilePage']);
  }

  ngOnInit() {
    this.editForm = this.fb.group({
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

  cancel() {
    this.router.navigate(['profilePage']);
  }
  photoFile!: File;
  onPhotoSelected(event: any) {
    this.photoFile = <File>event.target.files[0];
  }
  onFileChange(event: any) {
    if (event.target.files.length > 0) {
      this.selectedFile = event.target.files[0];
    }
  }
}
