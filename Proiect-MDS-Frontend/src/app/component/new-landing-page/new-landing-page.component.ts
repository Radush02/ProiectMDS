import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { Component } from '@angular/core';
import {
  Form,
  FormBuilder,
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { RouterOutlet, Router } from '@angular/router';
import { PostService } from '../../services/post.service';
import { forkJoin } from 'rxjs';
import { Profile } from '../../Profile';
import { jwtDecode } from 'jwt-decode';
import { UserService } from '../../services/user.service';
import { S3Service } from '../../services/s3.service';
import { NavbarComponent } from '../navbar/navbar.component';
import { OnInit } from '@angular/core';
import { OpenaiService } from '../../services/openai.service';
@Component({
  selector: 'app-new-landing-page',
  standalone: true,
  imports: [
    CommonModule,
    RouterOutlet,
    HttpClientModule,
    ReactiveFormsModule,
    NavbarComponent,
  ],
  providers: [PostService, UserService, S3Service, UserService,OpenaiService],
  templateUrl: './new-landing-page.component.html',
  styleUrl: './new-landing-page.component.css',
})
export class NewLandingPageComponent implements OnInit {
  profile: Profile = {
    nume: 'dummyLastName',
    prenume: 'dummyFirstName',
    email: 'dummyEmail',
    username: 'dummyUsername',
    nrTelefon: 'dummyPhoneNumber',
    dataNasterii: '2000-01-01',
    linkPozaProfil: '',
    carteIdentitate: false,
    permis: false,
    puncteFidelitate: 0,
  };
  findForm!: FormGroup;
  errorMessage = '';
  rangeAn: number[];
  constructor(
    public router: Router,
    private fb: FormBuilder,
    private PostService: PostService,
    private s3Service: S3Service,
    private userService: UserService,
    private openaiService: OpenaiService
  ) {
    const anCurent = new Date().getFullYear();
    this.rangeAn = this.rangeAni(1900, anCurent);

    this.findForm = this.fb.group({
      marca: [null, Validators.required],
      model: [null],
      anSelectatMinim: [null],
      anSelectatMaxim: [null],
      minimPret: [null],
      maximPret: [null],
      minimKm: [null],
      maximKm: [null],
    });
    let username = this.getProfile();
    console.log(username);
  }

  onFileChange(a: any) {
    console.log('a');
  }

  applyFilter() {
    let postKm = this.PostService.getPostsByKm(
      this.findForm.value.minimKm,
      this.findForm.value.maximKm
    );
    let postPret = this.PostService.getPostsByPrice(
      this.findForm.value.minimPret,
      this.findForm.value.maximPret
    );
    let postAn = this.PostService.getPostsByAge(
      this.findForm.value.anSelectatMinim,
      this.findForm.value.anSelectatMaxim
    );
    let postMarca = this.PostService.getPostsByBrand(this.findForm.value.marca);
    let postModel = this.PostService.getPostsByModel(this.findForm.value.model);

    forkJoin([postKm, postPret, postAn, postMarca, postModel]).subscribe(
      (responses) => {
        console.log(responses);
        let arrKm = responses[0];
        let arrPret = responses[1];
        let arrAn = responses[2];
        let arrMarca = responses[3];
        let arrModel = responses[4];

        function exista(array: carDTO[], obj: carDTO) {
          return array.some(
            (item) => JSON.stringify(item) === JSON.stringify(obj)
          );
        }

        function apareInToate(obj: carDTO) {
          return (
            exista(arrKm, obj) &&
            exista(arrPret, obj) &&
            exista(arrAn, obj) &&
            exista(arrMarca, obj) &&
            exista(arrModel, obj)
          );
        }
        let rez = arrAn.filter(apareInToate);
        console.log(rez);
        let baseUrl = window.location.origin + window.location.pathname;
        let resultsPageUrl = baseUrl + '/search-results';

        let resultsPage = this.router.navigate(['/searchResults'], {
          state: { results: rez },
        });
      }
    );
  }
  rangeAni(start: number, end: number): number[] {
    const ani = [];
    for (let an = start; an <= end; an++) {
      ani.push(an);
    }
    ani.sort((a, b) => b - a);
    return ani;
  }
  ngOnInit() {
    const anCurent = new Date().getFullYear();
    this.findForm = this.fb.group({
      marca: [null, Validators.required],
      model: [null],
      anSelectatMinim: [null],
      anSelectatMaxim: [null],
      minimPret: [null],
      maximPret: [null],
      minimKm: [null],
      maximKm: [null],
    });
    this.getPosts();
  }

  username = '';
  getProfile() {
    const token = localStorage.getItem('token');
    if (token) {
      const decodedToken: { [key: string]: any } = jwtDecode(token);
      this.username =
        decodedToken[
          'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'
        ];
      const role =
        decodedToken[
          'http://schemas.microsoft.com/ws/2008/06/identity/claims/role'
        ];

      console.log('Username:', this.username);
      console.log('Role:', role);
      this.userService.getUserDetails(this.username).subscribe((response) => {
        console.log(response);
        this.profile = {
          nume: response.nume,
          prenume: response.prenume,
          email: response.email,
          username: response.username,
          nrTelefon: response.nrTelefon,
          dataNasterii: response.dataNasterii.slice(0, 10),
          linkPozaProfil: response.linkPozaProfil,
          carteIdentitate: response.carteIdentitate,
          permis: response.permis,
          puncteFidelitate: response.puncteFidelitate,
        };
      });
    } else {
      console.error('Token not found in local storage');
    }
  }
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
  getUserInfo() {
    this.userService.getUser(this.user).subscribe((response) => {
      this.userInfo = response;
      this.userInfo.dataNasterii = this.userInfo.dataNasterii.slice(0, 10);
      console.log(this.userInfo);
    });
  }

  getImageUrl(imageName: string): string {
    return this.s3Service.getObjectUrl('dawbucket', imageName + '_pfp.png');
  }
  
  getUrl(fileImageName: string) {
    return this.s3Service.getObjectUrl('dawbucket', fileImageName);
  }
  test() {
    this.s3Service
      .getFilesFromFolder('dawbucket', 'post5/')
      .then((response) => {
        console.log(this.getUrl(response[0]));
      });
  }
  getPosts() {
    this.PostService.getPosts().subscribe((response) => {
      this.posts = response;
      console.log(this.posts);
    });
  }
  redirectToListingPage(titlu: any): void {
    this.router.navigate(['/listing'], { queryParams: { id: titlu } });
  }
  customSearch() {
    let prmpt = document.getElementById('customSearchText') as HTMLInputElement;
    console.log(prmpt);
    console.log(prmpt.value);
    this.openaiService.customSearch({prompt:prmpt.value}).subscribe((response) => {
      console.log(response);
      this.router.navigate(['/searchResults'], {
        state: { results: response },
      });
    });
  }
}

interface carDTO {
  id:number;
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
