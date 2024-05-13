import { Component, OnInit } from '@angular/core';
import { RouterOutlet, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import {
  FormBuilder,
  FormGroup,
  Validators,
  ReactiveFormsModule,
} from '@angular/forms';
import { UserService } from '../../services/user.service';
import { CookieService } from 'ngx-cookie-service';
import {
  validatorParola,
  validatorVarsta,
  validatorPoza,
} from '../../validators/user.validator';
@Component({
  selector: 'app-landing-page',
  standalone: true,
  imports: [CommonModule, RouterOutlet, HttpClientModule, ReactiveFormsModule],
  providers: [UserService],
  templateUrl: './landing-page.component.html',
  styleUrl: './landing-page.component.css',
})
export class LandingPageComponent implements OnInit {
  constructor(public router: Router, private cookieService: CookieService) {}
  ngOnInit(): void {
    if (this.cookieService.get('token') == '') {
      this.router.navigate(['/login']);
    }
  }
}
