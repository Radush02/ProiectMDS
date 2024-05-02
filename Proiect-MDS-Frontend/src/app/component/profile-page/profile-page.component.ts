import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { Output } from '@angular/core';
import { EventEmitter } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon';
import { Profile } from '../../Profile';
import { Dialog } from '@angular/cdk/dialog';
import { MatDialog } from '@angular/material/dialog';
import { EditProfileComponent } from '../edit-profile/edit-profile.component';
import { jwtDecode } from 'jwt-decode';
import { UserService } from '../../services/user.service';
import { HttpClientModule } from '@angular/common/http';
import { S3Service } from '../../services/s3.service';
@Component({
  selector: 'app-profile-page',
  standalone: true,
  imports: [MatButtonModule, MatIcon, HttpClientModule],
  providers: [UserService, S3Service],
  templateUrl: './profile-page.component.html',
  styleUrl: './profile-page.component.css',
})
export class ProfilePageComponent {
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
  };

  constructor(
    private router: Router,
    private dialog: MatDialog,
    private userServices: UserService,
    private s3Service: S3Service
  ) {
    let username = this.getProfile();
    console.log(username);
  }
  getImageUrl(imageName: string): string {
    return this.s3Service.getObjectUrl('dawbucket', imageName + '_pfp.png');
  }
  username="";
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
        };
      });
    } else {
      console.error('Token not found in local storage');
    }
  }

  //   editProfile(profile: Profile): void {
  //     this.router.navigate(['editPage']);
  //   }
}
