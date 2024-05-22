import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PostService } from '../../services/post.service';
import { S3Service } from '../../services/s3.service';
import { CommonModule } from '@angular/common';
import { NavbarComponent } from '../navbar/navbar.component';
import { CarouselModule } from 'ngx-bootstrap/carousel';
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
  constructor(private s3Service: S3Service,private postService:PostService,private params:ActivatedRoute) {
    this.params.queryParams.subscribe((params) => {
      this.carId = params['id'];
    });
    this.images = [];
  }
  getCar(id:number){
    this.postService.getPostById(id+1).subscribe(
      (result) => {
        this.result = result;
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
