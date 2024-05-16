import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PostService } from '../../services/post.service';
import { S3Service } from '../../services/s3.service';
import { CommonModule } from '@angular/common';
import { NavbarComponent } from '../navbar/navbar.component';

@Component({
  selector: 'app-carimg',
  standalone: true,
  imports: [CommonModule,NavbarComponent],
  providers: [S3Service, PostService],
  templateUrl: './carimg.component.html',
  styleUrl: './carimg.component.css'
})
export class CarimgComponent implements OnInit{
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
  }
}
