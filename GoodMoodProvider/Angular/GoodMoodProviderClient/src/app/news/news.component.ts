import { GetNewsResponse } from './../responses/getNewsReponse';
import { News } from '../classes/news';
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
  },
  {
    id: '2',
    article: 'aaaaaaqqq',
    plainText: 'eew',
    source: 'tut.by',
    datePosted: '01.02.03',
    rating: 21,
  }];

  selectedNews: News;
onSelect(news: News): void {
  this.selectedNews = news;
}

  getNews(): void {
    this.newsService.getNews()
        .subscribe( response => this.newsList = response);
  }

  ngOnInit() {
     this.getNews();
  }

}
