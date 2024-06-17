import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PostService } from '../../services/post.service';
import { S3Service } from '../../services/s3.service';
import { CommonModule } from '@angular/common';
import { NavbarComponent } from '../navbar/navbar.component';
import { CarouselModule } from 'ngx-bootstrap/carousel';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { SelectCardDialogComponent } from '../select-card-dialog/select-card-dialog.component';
import { DatePickerDialogComponent } from '../date-picker-dialog/date-picker-dialog.component';
import { ChirieService } from '../../services/chirie.service';
import { MatDialog } from '@angular/material/dialog';
import { HttpClientModule } from '@angular/common/http';
import { CardService } from '../../services/card.service';
//import { AddChirieService } from '../../services/add-chirie.service';
import { UserService } from '../../services/user.service';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { jwtDecode } from 'jwt-decode';
import { Profile } from '../../Profile';
import { Observable, Observer } from 'rxjs';
import { MessagePopUpComponent } from '../message-pop-up/message-pop-up.component';

@Component({
  selector: 'app-carimg',
  standalone: true,
  imports: [CommonModule, NavbarComponent, CarouselModule, HttpClientModule],
  providers: [S3Service, PostService, ChirieService, UserService, CardService],
  templateUrl: './carimg.component.html',
  styleUrls: ['./carimg.component.css'],
})
export class CarimgComponent implements OnInit {
  //Profile dummy data
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
  postOwner:any;
  loggedInUserId: number | null = null;
  carId = 0;
  images: string[] = [];
  result: any;
  loaded = false;
  username: string = '';
  userId2: number = 0;
  postId: number = 0;

  constructor(
    private s3Service: S3Service,
    private postService: PostService,
    private params: ActivatedRoute,
    private sanitizer: DomSanitizer,
    private dialog: MatDialog,
    private chirieService: ChirieService,
    //private addChirieService: AddChirieService,
    private userService: UserService,
    private cardService: CardService,
    public router: Router,
    private cookieService: CookieService
  ) {
    this.params.queryParams.subscribe((params) => {
      this.carId = params['id'];
    });
  }

  // Navigare
  gotoProfile(){
    this.router.navigate(['/profile'], { queryParams: { user: this.postOwner.username } });
  }

  // Ia URL imaginii
  getImageUrl(imageName: string): string {
    return this.s3Service.getObjectUrl('dawbucket', imageName + '_pfp.png');
  }
  // Dupa ce se da click pe butonul de inchiriaza
  openRentCarDialog(): void {
    console.log('postId:', this.postId);
    const selectCardDialogRef = this.dialog.open(SelectCardDialogComponent);

    selectCardDialogRef.afterClosed().subscribe((card) => {
      if (card) {
        const postareId = card.id;

        const datePickerDialogRef = this.dialog.open(DatePickerDialogComponent);

        datePickerDialogRef.afterClosed().subscribe((dates) => {
          if (dates) {
            const formattedStartDate = this.formatDate(dates.startDate);
            const formattedEndDate = this.formatDate(dates.endDate);

            const chirie = {
              userId: this.userId2,
              postareId: this.postId+1,
              dataStart: formattedStartDate,
              dataStop: formattedEndDate,
            };
            console.log(chirie);
            this.chirieService.addChirie(chirie).subscribe(
              (response) => {
                console.log('Chirie adăugată:', response);

                this.chirieService.rentEmailConfirmation(chirie).subscribe(
                  () => {
                    this.dialog.open(MessagePopUpComponent, {
                      data: 'Email sent. Please check your e-mail.',
                    });
                  },
                  (error) => {
                    console.log(error);
                  }
                );
              },
              (error) => {
                this.dialog.open(MessagePopUpComponent, {
                  data: 'Eroare in rezervare:'+error.error,
                });
              }
            );
          }
        });
      }
    });
  }

  // Helper method to format date to 'yyyy-mm-dd' format
  private formatDate(date: Date): string {
    const year = date.getFullYear();
    const month = ('0' + (date.getMonth() + 1)).slice(-2); // Adding 1 because getMonth() returns 0-based index
    const day = ('0' + date.getDate()).slice(-2);
    return `${year}-${month}-${day}`;
  }

  getCar(id: number) {
    this.postService.getPostById(id).subscribe(
      (result) => {
        this.result = result;
        this.getOwner(this.result.userId);
        this.result.linkMaps = this.sanitizer.bypassSecurityTrustResourceUrl(
          this.result.linkMaps
        );
        console.log(this.result);
        this.loaded = true;
      },
      (error) => {
        console.error('Car not found:', error);
      }
    );
  }

  getUrl(fileImageName: string) {
    return this.s3Service.getObjectUrl('dawbucket', fileImageName);
  }

  getCurrentUserIdPostId(): Observable<number> {
    return new Observable<number>((observer: Observer<number>) => {
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
        this.userService.getUser(this.username).subscribe(
          (response) => {
            console.log('raspuns get current user', response.id);
            this.userId2 = response.id;
            this.getPostId(this.carId);
            observer.next(response.id);

            observer.complete();
          },
          (error) => {
            console.error('Error fetching user:', error);
            observer.error(error);
          }
        );
      } else {
        console.error('Token not found in local storage');
        observer.error('Token not found in local storage');
      }
    });
  }

  getPostId(id: number) {
    this.postService.getPostById(id).subscribe(
      (result) => {
        if (result && result.id !== undefined) {
          this.postId = result.id;
          console.log('postId:', this.postId);
          this.load();
        } else {
          console.error('postId not found in result object');
        }

        this.result = result;
        this.result.linkMaps = this.sanitizer.bypassSecurityTrustResourceUrl(
          this.result.linkMaps
        );
        console.log(this.result);
        this.loaded = true;
      },
      (error) => {
        console.error('Car not found:', error);
      }
    );
  }

  load() {
    
    this.getCar(this.carId);
    this.s3Service
      .getFilesFromFolder('dawbucket', `post${this.carId}/`)
      .then((response: string[]) => {
        for (const r of response) {
          this.images.push(this.getUrl(r));
        }
      });
  }
  getOwner(id:number){
    this.userService.getByID(id).subscribe((response)=>{
      this.postOwner=response;
      console.log(response);
    });
  }
  ngOnInit() {
    this.getCurrentUserIdPostId().subscribe();
  }
}
