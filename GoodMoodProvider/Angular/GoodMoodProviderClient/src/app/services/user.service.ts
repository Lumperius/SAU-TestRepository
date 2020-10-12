import { LoginComponent } from './../login/login.component';
import { RegistrationRequest } from '../models/registrationRequest';
import { LoginRequest } from '../models/loginRequest';
import { Observable, of} from 'rxjs';
import { User } from '../models/user';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { catchError, map, tap } from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class UserService {

  loginUrl = 'https://localhost:44336/api/user/authenticate';
  registerUrl = 'https://localhost:44336/api/user/register';
  refreshUrl = 'https://localhost:44336/api/user/refresh/';
  userUrl = 'https://localhost:44336/api/user/';

  constructor(private http: HttpClient) { }

  loginUser(request: LoginRequest): Observable<User> {
    return this.http.post<User>(this.loginUrl, request)
    .pipe(tap(_ =>
      catchError(this.handleError<User>('loginUser'))
      ));
 }

  registerUser(request: RegistrationRequest): Observable<User> {
   return this.http.post<User>(this.registerUrl, request)
   .pipe(tap(_ =>
    catchError(this.handleError<User>('registerUser'))
    ));
}

  getUserById(id: string): Observable<User> {
    return this.http.get<User>(this.userUrl + id)
    .pipe(tap(_ =>
     catchError(this.handleError<User>('getUserById'))
     ));
  }

  putUser(user: User): void {
    this.http.post<User>(this.userUrl, user);
  }

  httpRefreshToken(): Observable<string>{
    if (localStorage.getItem('id') === undefined) {
      return undefined;
    }
    return this.http.get<string>(this.refreshUrl + localStorage.getItem('id'))
      .pipe(tap(_ =>
       catchError(this.handleError<string>('refreshUser'))
      ));
  }

  saveTokenAndIdInLocalStorage(user: User): void{
   localStorage.setItem('token', user.token);
   localStorage.setItem('id', user.id);
}


 private handleError<T>(operation = 'operation', result?: T) {
   return (error: any): Observable<T> => {
     console.error(error);
     return of(result as T);
    };
  }
}
