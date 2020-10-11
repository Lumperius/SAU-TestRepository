import { GetNewsResponse } from './../responses/getNewsReponse';
import { News } from '../models/news';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class NewsService {

  getRequestUrl = 'https://localhost:44336/api/News/';
  clearUrl = 'https://localhost:44336/api/News/clear';
  constructor(private http: HttpClient) { }

  getNews(from: number, count: number): Observable<News[]> {
    return this.http.get<News[]>(this.getRequestUrl + 'from=' + from + 'count=' + count)
    .pipe(tap(_ =>
       catchError(this.handleError<News[]>('getNews', [])))
    );
  }

  clearNewsInDatabase(){
     this.http.delete(this.clearUrl)
    .pipe(tap(_ =>
      catchError(this.handleError<News[]>('getNews', []))
      ));
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(error);
      return of(result as T);
    };
  }
}
