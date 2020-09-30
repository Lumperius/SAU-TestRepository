import { NewsService } from './services/news.service';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule} from '@angular/common/http';

import { AppComponent } from './app.component';
import { NewsComponent } from './news/news.component';
import { TopBarComponent } from './topBar/top-bar.component';

@NgModule({
  declarations: [
    AppComponent,
    NewsComponent,
    TopBarComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
  ],
  providers: [NewsService],
  bootstrap: [AppComponent]
})
export class AppModule { }
