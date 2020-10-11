import { GetNewsResponse } from './../responses/getNewsReponse';
import { News } from '../models/news';
import { NewsService } from './../services/news.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-news',
  templateUrl: './news.component.html',
  styleUrls: ['./news.component.css']
})
export class NewsComponent implements OnInit {

  constructor(private newsService: NewsService) { }

  page = 1;
  pageSize = 20;
  newsList: News[];

  selectedNews: News;

  onSelect(news: News): void {
    this.selectedNews = news;
}

  getNewsForPage(): void {
    this.newsService.getNews(this.pageSize * this.page, this.pageSize)
    .subscribe(response => this.newsList = response);
  }
  ngOnInit() {
    this.getNewsForPage();
  }
}
