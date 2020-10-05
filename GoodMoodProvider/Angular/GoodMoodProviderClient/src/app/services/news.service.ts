import { GetNewsResponse } from './../responses/getNewsReponse';
import { News } from '../classes/news';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class NewsService {

  getRequestUrl = 'https://localhost:44336/api/News/GetAll?count=';
  constructor(private http: HttpClient) { }

  getNews(count: number): Observable<News[]> {
    return this.http.get<News[]>(this.getRequestUrl + count)
    .pipe(tap(_ =>
       catchError(this.handleError<News[]>('getNews', [])))
    );
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(error);
      return of(result as T);
    };
  }
}
