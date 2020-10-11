import { News } from '../models/news';
import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-news-detail',
  styleUrls: ['./news-detail.component.css'],
  template: `<div [innerHTML]=news.body></div>`
})
export class NewsDetailComponent implements OnInit {

  @Input() news: News ;

  constructor() { }

  ngOnInit(): void {
  }

}
