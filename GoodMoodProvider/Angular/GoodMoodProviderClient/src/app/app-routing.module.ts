import { ExitComponent } from './exit/exit.component';
import { RoleGuardService } from './services/role-guard.service';
import { AdminComponent } from './admin/admin.component';
import { AuthGuardService } from './services/auth-guard.service';
import { ProfileComponent } from './profile/profile.component';
import { RegistrationComponent } from './registration/registration.component';
import { LoginComponent } from './login/login.component';
import { NewsComponent } from './news/news.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {path: 'news', component: NewsComponent},
  {path: 'login', component: LoginComponent},
  {path: 'registration', component: RegistrationComponent},
  {
    path: 'profile',
    component: ProfileComponent,
    canActivate: [AuthGuardService]
  },
  {
    path: 'exit',
    component: ExitComponent,
    canActivate: [AuthGuardService]
  },

  {
    path: 'admin',
    component: AdminComponent,
    canActivate: [RoleGuardService],
    data: {
    expectedRole: 'Admin'
    }
  }
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
