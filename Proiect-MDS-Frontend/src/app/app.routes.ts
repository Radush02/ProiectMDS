import { Routes } from '@angular/router';
import { LoginComponent } from './component/login/login.component';
import { LandingPageComponent } from './component/landing-page/landing-page.component';
import { RegisterComponent } from './component/register/register.component';

export const routes: Routes = [
  { path: '', component: LoginComponent },
  { path: 'login', component: LoginComponent },
  { path: 'landingPage', component: LandingPageComponent },
  { path: 'register', component: RegisterComponent },
];
