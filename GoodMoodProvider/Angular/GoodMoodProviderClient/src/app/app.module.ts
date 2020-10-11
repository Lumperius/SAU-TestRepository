import { NewsService } from './services/news.service';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule} from '@angular/common/http';

import { AppComponent } from './app.component';
import { NewsComponent } from './news/news.component';
import { TopBarComponent } from './topBar/top-bar.component';
import { NewsDetailComponent } from './news-detail/news-detail.component';
import { AppRoutingModule } from './app-routing.module';
import { RegistrationComponent } from './registration/registration.component';
import { LoginComponent } from './login/login.component';
import { ProfileComponent } from './profile/profile.component';
import { AdminComponent } from './admin/admin.component';
import { ExitComponent } from './exit/exit.component';

@NgModule({
  declarations: [
    AppComponent,
    NewsComponent,
    TopBarComponent,
    NewsDetailComponent,
    RegistrationComponent,
    LoginComponent,
    ProfileComponent,
    AdminComponent,
    ExitComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    AppRoutingModule,
  ],
  providers: [NewsService],
  bootstrap: [AppComponent]
})
export class AppModule { }
