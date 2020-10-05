import { RegistrationComponent } from './registration/registration.component';
import { LoginComponent } from './login/login.component';
import { OfficeComponent } from './office/office.component';
import { NewsComponent } from './news/news.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';


const routes: Routes = [
  {path: 'news', component: NewsComponent},
  {path: 'office', component: OfficeComponent},
  {path: 'login', component: LoginComponent},
  {path: 'registration', component: RegistrationComponent},
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes)
  ],
  exports: [
    RouterModule
  ]
})

export class AppRoutingModule { }
