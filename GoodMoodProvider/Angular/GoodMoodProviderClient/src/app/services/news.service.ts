import { GetNewsResponse } from './../responses/getNewsReponse';
import { News } from './../news';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class NewsService {

  newsUrl = 'https://localhost:44336/api/News/GetAll';

  constructor(private http: HttpClient) { }

  getNews(): Observable<News[]> {
    return this.http.get<News[]>(this.newsUrl)
    .pipe(
      tap(_ => catchError(this.handleError<GetNewsResponse[]>('getHeroes', [])))
    );
  }

  getTest(): Observable<string> {
    return this.http.get<string>(this.newsUrl);
  }


    private handleError<T>(operation = 'operation', result?: T) {
      return (error: any): Observable<T> => {
        console.error(error);
        return of(result as T);
      };
    }

}
