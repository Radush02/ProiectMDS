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
@Component({
  selector: 'app-add-car',
  templateUrl: './add-car.component.html',
  imports: [CommonModule, RouterOutlet, HttpClientModule, ReactiveFormsModule],
  providers: [CarService],
  styleUrls: ['./add-car.component.css'],
  standalone: true,
})
export class AddCarComponent implements OnInit {
  carForm: FormGroup;
  errorMessage = '';
  constructor(
    private fb: FormBuilder,
    private httpClient: HttpClient,
    public router: Router,
    private carService: CarService,

  ) {
    this.carForm = this.fb.group({
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


