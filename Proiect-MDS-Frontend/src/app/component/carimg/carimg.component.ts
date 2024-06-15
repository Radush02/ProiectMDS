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
import { UserService } from '../../services/user.service';
;

@Component({
  selector: 'app-carimg',
  standalone: true,
  imports: [CommonModule,NavbarComponent,CarouselModule, HttpClientModule],
  providers: [S3Service, PostService,ChirieService, UserService, CardService],
  templateUrl: './carimg.component.html',
  styleUrl: './carimg.component.css'
})
export class CarimgComponent implements OnInit{
  loggedInUserId: number | null = null;
  carId=0;
  images: string[];
  result:any;
  loaded=false;
  constructor(private s3Service: S3Service,private postService:PostService,private params:ActivatedRoute,private sanitizer: DomSanitizer, private dialog: MatDialog, private chirieService: ChirieService, private userService: UserService, private cardService: CardService) {
    this.params.queryParams.subscribe((params) => {
      this.carId = params['id'];
    });
    this.images = [];
  }

  

  openRentCarDialog(): void {
    // const userId = this.userService.getUserId();
    const selectCardDialogRef = this.dialog.open(SelectCardDialogComponent);
  
    selectCardDialogRef.afterClosed().subscribe(card => {
      if (card) {
        const postareId = card.id;
  
        const datePickerDialogRef = this.dialog.open(DatePickerDialogComponent);
  
        datePickerDialogRef.afterClosed().subscribe(dates => {
          if (dates) {
            const chirie = {
              // userId: userId,
              postareId: postareId,
              dataStart: dates.dataStart,
              dataStop: dates.dataStop
            };
  
            this.chirieService.addChirie(chirie).subscribe(
              (response) => {
                console.log('Chirie adăugată:', response);
                // Poți adăuga aici orice altă logică necesară după ce s-a făcut cererea POST cu succes
              },
              (error) => {
                console.error('Eroare la adăugarea chiriei:', error);
              }
            );
          }
        });
      }
    });
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

  getUserId(id: number) {
    this.postService.getPostById(id).subscribe(
      (result) => {
        // Asigură-te că obiectul result este definit și conține proprietatea userId
        if (result && result.userId !== undefined) {
          // Salvează userId într-o variabilă sau folosește-l direct
          const userId = result.userId;
          console.log('userId:', userId);
  
          // În cazul în care vrei să faci ceva cu userId (de exemplu, să-l întorci)
          console.log('userId:', userId)
          return userId;
        } else {
          console.error('userId not found in result object');
        }
  
        this.result = result;
        this.result.linkMaps = this.sanitizer.bypassSecurityTrustResourceUrl(this.result.linkMaps);
        console.log(this.result);
        this.loaded = true;
      },
      (error) => {
        console.error('Car not found:', error);
      }
    );
  }

  getPostId(id: number) {
    this.postService.getPostById(id).subscribe(
      (result) => {
        // Asigură-te că obiectul result este definit și conține proprietatea id
        if (result && result.id !== undefined) {
          // Salvează id-ul într-o variabilă sau folosește-l direct
          const postId = result.id;
          console.log('postId:', postId);
  
          // În cazul în care vrei să faci ceva cu id-ul (de exemplu, să-l întorci)
          return postId;
        } else {
          console.error('postId not found in result object');
        }
  
        this.result = result;
        this.result.linkMaps = this.sanitizer.bypassSecurityTrustResourceUrl(this.result.linkMaps);
        console.log(this.result);
        this.loaded = true;
      },
      (error) => {
        console.error('Car not found:', error);
      }
    );
  }
  // getLoggedInUserId(): void {
  //   this.loggedInUserId = this.userService.getUserId();
  //   if (this.loggedInUserId !== null) {
  //     console.log('Logged in user ID:', this.loggedInUserId);
  //     // Poți face alte operații cu this.loggedInUserId aici
  //   } else {
  //     console.log('Utilizatorul nu este autentificat sau nu se poate obține userId.');
  //   }
  // }
  
  


  load() {
    // this.getLoggedInUserId();
    this.getPostId(this.carId);
    this.getUserId(this.carId);
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
