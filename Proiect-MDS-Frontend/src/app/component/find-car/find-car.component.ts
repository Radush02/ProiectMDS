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
@Component({
  selector: 'app-find-car',
  standalone: true,
  imports: [
    CommonModule,
    RouterOutlet,
    HttpClientModule,
    ReactiveFormsModule,
    NavbarComponent,
  ],
  providers: [PostService, UserService, S3Service],
  templateUrl: './find-car.component.html',
  styleUrl: './find-car.component.css',
})
export class FindCarComponent {
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
    puncteFidelitate:0,
  };
  findForm!: FormGroup;
  errorMessage = '';
  rangeAn: number[];
  constructor(
    public router: Router,
    private fb: FormBuilder,
    private PostService: PostService,
    private s3Service: S3Service,
    private userServices: UserService
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

        let resultsPage = this.router.navigate(['/searchResults'], { state: { results: rez } });

        // let resultsPage = window.open(resultsPageUrl);
        // if (resultsPage) {
        //   resultsPage.onload = () => {
        //     if (resultsPage) {
        //       resultsPage.document.write(
        //         '<html><head><title>Rezultate căutare</title></head><body>'
        //       );
        //       resultsPage.document.write('<h1>Rezultate căutare</h1>');
        //       resultsPage.document.write('<ul>');
        //       rez.forEach((car: carDTO) => {
        //         if (resultsPage)
        //           resultsPage.document.write(
        //             `<li>${car.titlu} - ${car.model}-${car.firma} - ${car.anFabricatie} -${car.descriere}</li>`
        //           );
        //       });
        //       resultsPage.document.write('</ul></body></html>');
        //       resultsPage.document.close();
        //       if (resultsPage) {
        //         resultsPage.postMessage(rez, baseUrl);
        //       }
        //     }
        //   };
        // } else {
        //   alert(
        //     'Popup blockat! Te rugăm să permiti pop-up-uri pentru a afișa rezultatele.'
        //   );
        // }
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
  NgOnInit() {
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
  }
  getImageUrl(imageName: string): string {
    return this.s3Service.getObjectUrl('dawbucket', imageName + '_pfp.png');
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
      this.userServices.getUserDetails(this.username).subscribe((response) => {
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
