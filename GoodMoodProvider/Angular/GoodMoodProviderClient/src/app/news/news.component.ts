import { GetNewsResponse } from './../responses/getNewsReponse';
import { News } from './../news';
import { NewsService } from './../services/news.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-news',
  templateUrl: './news.component.html',
  styleUrls: ['./news.component.css']
})
export class NewsComponent implements OnInit {

  constructor(private newsService: NewsService) { }

  newsList: News[] = [{
    id: '1',
    article: 'qqq',
    plainText: 'eee',
    source: 'tut.by',
    datePosted: '01.02.03',
    rating: 10,
  }];
  getResponse: GetNewsResponse[] = [{
    id: '2',
    article: 'qqq',
    plainText: 'eee',
    source: 'tut.by',
    rating: 11,
  }];
  test: string;

  getNews(): void {
    this.newsService.getNews()
        .subscribe( response => this.newsList = response);
  }

  testy(): void {
    this.newsService.getTest()
        .subscribe( response => this.test = response);
  }

  ngOnInit() {
     this.getNews();
     this.testy();
  }

}
