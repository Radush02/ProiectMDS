import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UserService } from '../../services/user.service';
import { CommonModule } from '@angular/common';
import { S3Service } from '../../services/s3.service';
import { NavbarComponent } from '../navbar/navbar.component';
@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [CommonModule, NavbarComponent],
  providers: [UserService, S3Service],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css',
})
export class ProfileComponent implements OnInit {
  user: string = 'user';
  loaded = false;
  userInfo: userDTO = {
    nume: '',
    prenume: '',
    username: '',
    nrTelefon: '',
    linkPozaProfil: '',
    dataNasterii: '',
    nrPostari: 0,
  };
  constructor(
    private params: ActivatedRoute,
    private userService: UserService,
    private s3Service: S3Service
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
}
interface userDTO {
  nume: string;
  prenume: string;
  username: string;
  nrTelefon: string;
  linkPozaProfil: string;
  dataNasterii: string;
  nrPostari: number;
}
