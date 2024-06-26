import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from '../../services/user.service';
import { CommonModule } from '@angular/common';
import { S3Service } from '../../services/s3.service';
import { NavbarComponent } from '../navbar/navbar.component';
import { PostService } from '../../services/post.service';
import { HttpClientModule } from '@angular/common/http';
import { CarouselModule } from 'ngx-bootstrap/carousel';
@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [CommonModule, NavbarComponent, HttpClientModule, CarouselModule],
  providers: [UserService, S3Service, PostService],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css',
})
export class ProfileComponent implements OnInit {
  user: string = 'user';
  loaded = false;
  userInfo: userDTO = {
    id: 0,
    nume: '',
    prenume: '',
    username: '',
    nrTelefon: '',
    linkPozaProfil: '',
    dataNasterii: '',
    nrPostari: 0,
  };
  posts: carDTO[] = [];
  constructor(
    private params: ActivatedRoute,
    private userService: UserService,
    private s3Service: S3Service,
    private PostService: PostService,
    private router: Router
  ) {
    this.params.queryParams.subscribe((params) => {
      this.user = params['user'];
    });
  }
  getUserInfo() {
    this.userService.getUser(this.user).subscribe((response) => {
      this.userInfo = response;
      this.userInfo.dataNasterii = this.userInfo.dataNasterii.slice(0, 10);
      console.log(this.userInfo);
    });
  }
  ngOnInit() {
    this.getUserInfo();
    this.loaded = true;
    this.getUserInfo();
    this.loaded = true;
    setTimeout(() => this.getPosts(), 500);
  }
  getImageUrl(imageName: string): string {
    return this.s3Service.getObjectUrl('dawbucket', imageName + '_pfp.png');
  }
  getUrl(fileImageName: string) {
    return this.s3Service.getObjectUrl('dawbucket', fileImageName);
  }
  goto(id:number){
    this.router.navigate(['/carimg'], { queryParams: { id: id } });
  }
  test() {
    this.s3Service
      .getFilesFromFolder('dawbucket', 'post5/')
      .then((response) => {
        console.log(this.getUrl(response[0]));
      });
  }
  getPosts() {
    this.PostService.getPostsById(this.userInfo.id).subscribe((response) => {
      this.posts = response;
    });
  }
}
interface userDTO {
  id: number;
  nume: string;
  prenume: string;
  username: string;
  nrTelefon: string;
  linkPozaProfil: string;
  dataNasterii: string;
  nrPostari: number;
}
interface carDTO {
  id:number;
  userid: number;
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
