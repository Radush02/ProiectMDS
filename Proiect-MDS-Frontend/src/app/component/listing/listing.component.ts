import { CommonModule } from '@angular/common';
import { Component, NgModule, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NavbarComponent } from '../navbar/navbar.component';
import { CarouselModule } from 'ngx-bootstrap/carousel';
import { S3Service } from '../../services/s3.service';
import { PostService } from '../../services/post.service';
import { HttpClient } from '@angular/common/http';
@Component({
  selector: 'app-listing',
  templateUrl: './listing.component.html',
  styleUrl: './listing.component.css',
})
export class ListingComponent implements OnInit {
  results: any[] = [];
  carId=0;
  images: string[];
  constructor(private s3Service: S3Service,private postService:PostService,private params:ActivatedRoute) {
    this.params.queryParams.subscribe((params) => {
      this.carId = params['id'];
    });
    this.images = [];
  }
  getUrl(fileImageName: string) {
    return this.s3Service.getObjectUrl('dawbucket', fileImageName);
  }
  test() {
    this.s3Service
      .getFilesFromFolder('dawbucket', `post${this.carId}/`)
      .then((response:string[]) => {
        for(let r in response){
          this.images.push(this.getUrl(response[r]));
        }
      });
  }
  ngOnInit(){
    this.test();
  }}

  @NgModule({
    declarations: [
      ListingComponent
    ],
    imports: [
      CommonModule,
      NavbarComponent,
      CarouselModule.forRoot()
    ]
    
  })
  export class ListingComponentModule { 
   

  

  }