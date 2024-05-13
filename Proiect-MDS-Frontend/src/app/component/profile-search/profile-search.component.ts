import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { NavbarComponent } from '../navbar/navbar.component';
import { UserService } from '../../services/user.service';
import { S3Service } from '../../services/s3.service';
import { CommonModule } from '@angular/common';
@Component({
  selector: 'app-profile-search',
  standalone: true,
  imports: [NavbarComponent, CommonModule],
  providers: [UserService, S3Service],
  templateUrl: './profile-search.component.html',
  styleUrls: ['./profile-search.component.css'],
})
export class ProfileSearchComponent {
  username = '';
  errorMessage = '';
  constructor(
    public router: Router,
    private cookieService: CookieService,
    private userService: UserService,
    private s3Service: S3Service
  ) {}

  ngOnInit(): void {
    if (this.cookieService.get('token') === '') {
      this.router.navigate(['/login']);
    }
  }

  searchProfile(username: string) {
    if (username) {
      this.userService.getUser(username).subscribe(
        (result) => {
          this.router.navigate(['/profile'], {
            queryParams: { user: username },
          });
        },
        (error) => {
          console.error('User not found:', error);
          if (error.status === 404) {
            this.errorMessage = 'User not found.';
          } else {
            this.errorMessage =
              'An error occurred while searching for the user.';
          }
        }
      );
    }
  }
}
