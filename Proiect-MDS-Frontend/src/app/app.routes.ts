import { Routes } from '@angular/router';
import { LoginComponent } from './component/login/login.component';
import { LandingPageComponent } from './component/landing-page/landing-page.component';

export const routes: Routes = [{path: 'login', component:LoginComponent},{path:"landingPage",component:LandingPageComponent}];
