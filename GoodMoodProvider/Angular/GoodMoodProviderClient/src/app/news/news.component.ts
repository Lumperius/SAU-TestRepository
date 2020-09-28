import { Component, OnInit } from '@angular/core';
import { NEWS } from 'mock/mock-news'

@Component({
  selector: 'app-news',
  templateUrl: './news.component.html',
  styleUrls: ['./news.component.css']
})
export class NewsComponent implements OnInit {

  constructor() { }

  newsList = NEWS;
  title = 'Howdy!';

  ngOnInit() {

  }

}


