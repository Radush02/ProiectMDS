import { Routes } from '@angular/router';
import { LoginComponent } from './component/login/login.component';
import { LandingPageComponent } from './component/landing-page/landing-page.component';
import { RegisterComponent } from './component/register/register.component';
import { ProfilePageComponent } from './component/profile-page/profile-page.component';
import { EditProfileComponent } from './component/edit-profile/edit-profile.component';

export const routes: Routes = [
  { path: '', component: ProfilePageComponent },
  { path: 'login', component: LoginComponent },
  { path: 'landingPage', component: LandingPageComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'profilePage', component: ProfilePageComponent },
  { path: 'editPage', component: EditProfileComponent },
];
