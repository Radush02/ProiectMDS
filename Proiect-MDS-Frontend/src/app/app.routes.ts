import { Routes } from '@angular/router';
import { LoginComponent } from './component/login/login.component';
import { LandingPageComponent } from './component/landing-page/landing-page.component';
import { RegisterComponent } from './component/register/register.component';
import { EditProfileComponent } from './component/edit-profile/edit-profile.component';
import { FindCarComponent } from './component/find-car/find-car.component';
import { AddCarComponent } from './component/add-car/add-car.component';
import { ProfilePageComponent } from './component/profile-page/profile-page.component';
import { CustomerSupportComponent } from './component/customer-support/customer-support.component';


export const routes: Routes = [
  { path: '', component: ProfilePageComponent },
  { path: 'login', component: LoginComponent },
  { path: 'landingPage', component: LandingPageComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'editPage', component: EditProfileComponent },
  { path: 'findCar', component: FindCarComponent },
  { path: 'addCar', component: AddCarComponent },
  { path: 'profilePage', component: ProfilePageComponent },
  { path: 'customerSupport', component: CustomerSupportComponent },

];
