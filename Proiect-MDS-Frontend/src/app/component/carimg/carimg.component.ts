import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PostService } from '../../services/post.service';
import { S3Service } from '../../services/s3.service';
import { CommonModule } from '@angular/common';
import { NavbarComponent } from '../navbar/navbar.component';
import { CarouselModule } from 'ngx-bootstrap/carousel';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';

@Component({
  selector: 'app-carimg',
  standalone: true,
  imports: [CommonModule,NavbarComponent,CarouselModule],
  providers: [S3Service, PostService],
  templateUrl: './carimg.component.html',
  styleUrl: './carimg.component.css'
})
export class CarimgComponent implements OnInit{
  carId=0;
  images: string[];
  result:any;
  loaded=false;
  constructor(private s3Service: S3Service,private postService:PostService,private params:ActivatedRoute,private sanitizer: DomSanitizer) {
    this.params.queryParams.subscribe((params) => {
      this.carId = params['id'];
    });
    this.images = [];
  }
  getCar(id:number){
    id++;
    this.postService.getPostById(id).subscribe(
      (result) => {
        this.result = result;
        this.result.linkMaps = this.sanitizer.bypassSecurityTrustResourceUrl(this.result.linkMaps);
        console.log(this.result);
        this.loaded=true;
      },
      (error) => {
        console.error('Car not found:', error);
      }
    );
  }
  getUrl(fileImageName: string) {
    return this.s3Service.getObjectUrl('dawbucket', fileImageName);
  }
  
  load() {

    this.getCar(this.carId);
    this.s3Service
      .getFilesFromFolder('dawbucket', `post${this.carId}/`)
      .then((response:string[]) => {
        for(let r in response){
          this.images.push(this.getUrl(response[r]));
        }
      });
  
  }
  ngOnInit(){
    this.load();
  }
}
interface carDTO {
  titlu: string;
  descriere: string;
  pret: number;
  firma: string;
  model: string;
  culoare: string;
  kilometraj: number;
  anFabricatie: number;
  talon: string;
  carteIdentitateMasina: string;
  asigurare: string;
  adresa_formala: string;
  linkMaps: string;
}
