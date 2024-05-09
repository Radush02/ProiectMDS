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
@Component({
  selector: 'app-add-car',
  templateUrl: './add-car.component.html',
  imports: [CommonModule, RouterOutlet, HttpClientModule, ReactiveFormsModule],
  providers: [CarService,OpenaiService],
  styleUrls: ['./add-car.component.css'],
  standalone: true,
})
export class AddCarComponent implements OnInit {
  carForm: FormGroup;
  errorMessage = '';
  suggestion='';
  loading=false;
  constructor(
    private fb: FormBuilder,
    private httpClient: HttpClient,
    public router: Router,
    private carService: CarService,
    private cookieService: CookieService,
    private openaiService: OpenaiService
  ) {
    const decodedToken: { [key: string]: any } = jwtDecode(this.cookieService.get('token'))
    this.carForm = this.fb.group({
      userId: [decodedToken['id']],
      titlu: ['', Validators.required],
      descriere: ['', Validators.required],
      pret: ['', Validators.required],
      firma: ['', Validators.required],
      model: ['', Validators.required],
      kilometraj: ['', Validators.required],
      anFabricatie: ['', Validators.required],
      talon: ['', Validators.required],
      carteIdentitateMasina: ['', Validators.required],
      asigurare: ['', Validators.required],
    });
  }
  ngOnInit(): void {}

  async enhanceDescription(){
    this.loading=true;
    if(this.carForm.value.descriere==''){
      alert('Includeti o descriere!');
      return;
    }
    console.log({prompt:this.carForm.value.descriere});
    this.openaiService.response({prompt:this.carForm.value.descriere}).subscribe((response:any) => {
      this.suggestion=response.prompt;
    });

    console.log(this.suggestion);
    this.loading=false;
  }
  // enhanceDescription(){
  //   if(this.carForm.value.descriere==''){
  //     alert('Includeti o descriere!');
  //   }
  //   const enhanceButton = document.getElementById('enhance') as HTMLButtonElement;
  //   if (enhanceButton) {
  //     enhanceButton.disabled = true;
  //   }
  //   this.openaiService.response(this.carForm.value.descriere,this.descriptionContext)
  //   .then((response:any) => {
  //     this.suggestion=response.choises[0].message.content;
  //   })
  //   .catch((error:any) => {
  //     console.error(error);
  //     this.errorMessage = 'A aparut o eroare';
  //   });
  // }
  addCar() {
    if (this.carForm.invalid) {
      return;
    }

    this.carService.addCar(this.carForm.value)
      .subscribe(
        (response: any) => { 

          console.log('Mașina a fost adăugată cu succes!', response);
          
          this.carForm.reset(); 
        },
        (error:any) => {
         
          console.error('A apărut o eroare în timpul adăugării mașinii:', error);
          this.errorMessage = 'A apărut o eroare în timpul adăugării mașinii. Vă rugăm să încercați din nou.';
        }
      );
  }
}


