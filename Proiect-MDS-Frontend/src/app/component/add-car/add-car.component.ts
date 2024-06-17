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
import { CarService } from '../../services/add-car.service';
import { CookieService } from 'ngx-cookie-service';
import { jwtDecode } from 'jwt-decode';
import { OpenaiService } from '../../services/openai.service';
import { NavbarComponent } from '../navbar/navbar.component';
import { S3Service } from '../../services/s3.service';
import {ClipboardModule} from '@angular/cdk/clipboard';

@Component({
  selector: 'app-add-car',
  templateUrl: './add-car.component.html',
  imports: [
    CommonModule,
    RouterOutlet,
    HttpClientModule,
    ReactiveFormsModule,
    NavbarComponent,
    ClipboardModule
  ],
  providers: [CarService, OpenaiService],
  styleUrls: ['./add-car.component.css'],
  standalone: true,
})
export class AddCarComponent implements OnInit {
  carForm: FormGroup;
  errorMessage = '';
  suggestion='';
  selectedFiles: File[] = [];
  loading=false;
  constructor(
    private fb: FormBuilder,
    private httpClient: HttpClient,
    public router: Router,
    private carService: CarService,
    private cookieService: CookieService,
    private openaiService: OpenaiService,
    private s3Service: S3Service
  ) {
    const decodedToken: { [key: string]: any } = jwtDecode(
      this.cookieService.get('token')
    );
    this.carForm = this.fb.group({
      userId: [decodedToken['id']],
      titlu: [null, Validators.required],
      descriere: [null, Validators.required],
      pret: [null, Validators.required],
      firma: [null, Validators.required],
      model: [null, Validators.required],
      kilometraj: [null, Validators.required],
      anFabricatie: [null, Validators.required],
      talon: [null, Validators.required],
      carteIdentitateMasina: [null, Validators.required],
      asigurare: [null, Validators.required],
      imagini: [null, Validators.required],
      adresa: [null, Validators.required],
      culoare: [null, Validators.required],
      linkMaps: ["value", Validators.required],
      adresa_formala: ["value", Validators.required],

    });
  }
  onFileSelected(event: any) {
    const files: FileList = event.target.files;
    for (let i = 0; i < files.length; i++) {
      this.selectedFiles.push(files.item(i)!);
    }
  }
// Functie de copiere a mesajului generat de AI in clipboard
  copyToClipboard() {
    const textToCopy = this.suggestion;
    const textarea = document.createElement('textarea');
    textarea.style.position = 'fixed';
    textarea.style.opacity = '0';
    textarea.value = textToCopy;
    document.body.appendChild(textarea);
    textarea.select();
    document.execCommand('copy');
    document.body.removeChild(textarea);
    alert('Textul a fost copiat in clipboard!');
  }

  ngOnInit(): void {}

  async enhanceDescription() {
    this.loading = true;
    if (this.carForm.value.descriere == '') {
      alert('Includeti o descriere!');
      return;
    }
    console.log({ prompt: this.carForm.value.descriere });
    this.openaiService
      .response({ prompt: this.carForm.value.descriere })
      .subscribe((response: any) => {
        this.suggestion = response.prompt;
      });

    console.log(this.suggestion);
    this.loading = false;
  }

  // Functie de adaugare a unei masini
  addCar() {
    if (this.carForm.invalid) {
      return;
    }
    var form = new FormData();
    form.append('userId', this.carForm.value.userId);
    form.append('titlu', this.carForm.value.titlu);
    form.append('descriere', this.carForm.value.descriere);
    form.append('pret', this.carForm.value.pret);
    form.append('firma', this.carForm.value.firma);
    form.append('model', this.carForm.value.model);
    form.append('kilometraj', this.carForm.value.kilometraj);
    form.append('anFabricatie', this.carForm.value.anFabricatie);
    form.append('talon', this.carForm.value.talon);
    form.append('carteIdentitateMasina', this.carForm.value.carteIdentitateMasina);
    form.append('asigurare', this.carForm.value.asigurare);
    for (let i = 0; i < this.selectedFiles.length; i++) {
      form.append('imagini', this.selectedFiles[i]);
    }
    form.append('locatie', this.carForm.value.adresa);
    form.append('culoare', this.carForm.value.culoare);
    this.carForm.value.linkMaps="value";
    this.carForm.value.adresa_formala="value";
    form.append('locatie_formala',this.carForm.value.adresa_formala);
    form.append('linkMaps',this.carForm.value.linkMaps);
    console.log(form.get('imagini'));
    console.log(this.carForm.value);
    console.log(form);
    this.carService.addCar(form)
      .subscribe(
        (response: any) => {
          this.s3Service.uploadFile('dawbucket', `thumbnail${response}`, this.selectedFiles[0]);
        this.router.navigate(['/home']);
      },
      (error: any) => {
        console.error('A apărut o eroare în timpul adăugării mașinii:', error);
        this.errorMessage =
          'A apărut o eroare în timpul adăugării mașinii. Vă rugăm să încercați din nou.';
      }
    );
  }
}
