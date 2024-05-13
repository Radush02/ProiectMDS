import { Routes } from '@angular/router';
import { LoginComponent } from './component/login/login.component';
import { LandingPageComponent } from './component/landing-page/landing-page.component';
import { RegisterComponent } from './component/register/register.component';
import { EditProfileComponent } from './component/edit-profile/edit-profile.component';
import { FindCarComponent } from './component/find-car/find-car.component';
import { AddCarComponent } from './component/add-car/add-car.component';
import { ProfilePageComponent } from './component/profile-page/profile-page.component';
import { CustomerSupportComponent } from './component/customer-support/customer-support.component';
import { PasswordResetComponent } from './component/password-reset/password-reset.component';
import { ForgotPasswordComponent } from './component/forgot-password/forgot-password.component';
import { NavbarComponent } from './component/navbar/navbar.component';
import { ProfileComponent } from './component/profile/profile.component';

export const routes: Routes = [
  { path: '', component: LoginComponent },
  { path: 'login', component: LoginComponent },
  { path: 'landingPage', component: LandingPageComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'editPage', component: EditProfileComponent },
  { path: 'findCar', component: FindCarComponent },
  { path: 'addCar', component: AddCarComponent },
  { path: 'profilePage', component: ProfilePageComponent },
  { path: 'customerSupport', component: CustomerSupportComponent },
  { path: 'resetPassword', component: PasswordResetComponent },
  { path: 'forgotPassword', component: ForgotPasswordComponent },
  { path: 'navbar', component: NavbarComponent },
  {path:'profile',component:ProfileComponent}

];
