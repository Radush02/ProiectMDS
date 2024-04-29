import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { Component } from '@angular/core';
import { Form, FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { RouterOutlet, Router } from '@angular/router';
import { PostService } from '../../services/post.service';
import {forkJoin} from 'rxjs';
@Component({
  selector: 'app-find-car',
  standalone: true,
  imports: [CommonModule, RouterOutlet, HttpClientModule, ReactiveFormsModule],
  providers: [PostService],
  templateUrl: './find-car.component.html',
  styleUrl: './find-car.component.css'
})

export class FindCarComponent {
  findForm!: FormGroup;
  errorMessage = '';
  rangeAn : number[];
  constructor(public router:Router,private fb: FormBuilder,private PostService:PostService) {
    const anCurent = new Date().getFullYear();
    this.rangeAn = this.rangeAni(1900, anCurent);

    this.findForm = this.fb.group({
      marca: [null,Validators.required],
      model: [null],
      anSelectatMinim: [null],
      anSelectatMaxim: [null],
      minimPret: [null],
      maximPret: [null],
      minimKm: [null],
      maximKm: [null],
    });
  }

  onFileChange(a:any){
    console.log('a');
  }
  
  applyFilter(){
    let postKm = this.PostService.getPostsByKm(this.findForm.value.minimKm, this.findForm.value.maximKm);
    let postPret = this.PostService.getPostsByPrice(this.findForm.value.minimPret, this.findForm.value.maximPret);
    let postAn = this.PostService.getPostsByAge(this.findForm.value.anSelectatMinim, this.findForm.value.anSelectatMaxim);
    let postMarca = this.PostService.getPostsByBrand(this.findForm.value.marca);
    let postModel = this.PostService.getPostsByModel(this.findForm.value.model);
  
    forkJoin([postKm, postPret, postAn, postMarca, postModel]).subscribe((responses) => {
      let arrKm = responses[0];
      let arrPret = responses[1];
      let arrAn = responses[2];
      let arrMarca = responses[3];
      let arrModel = responses[4];
  
      function exista(array:carDTO[], obj:carDTO) {
        return array.some(item => JSON.stringify(item) === JSON.stringify(obj));
      }
  
      function apareInToate(obj: carDTO) {
        return exista(arrKm, obj) && exista(arrPret, obj) && exista(arrAn, obj) && exista(arrMarca, obj) && exista(arrModel, obj);
      }
  
      let rez = arrAn.filter(apareInToate);
      console.log(rez);
      return rez;
    });
  }
  rangeAni(start: number, end: number): number[] {
    const ani = [];
    for (let an = start; an <= end; an++) {
      ani.push(an);
    }
    ani.sort((a, b) => b - a);
    return ani;
  }
  NgOnInit(){
    const anCurent = new Date().getFullYear();
    this.findForm = this.fb.group({
      marca: [null,Validators.required],
      model: [null],
      anSelectatMinim: [null],
      anSelectatMaxim: [null],
      minimPret: [null],
      maximPret: [null],
      minimKm: [null],
      maximKm: [null],
    });
  }
}
interface carDTO {
  titlu: string;
  descriere: string;
  pret: number;
  firma: string;
  model: string;
  kilometraj: number;
  anFabricatie: number;
  talon: string;
  carteIdentitateMasina: string;
  asigurare: string;
}

