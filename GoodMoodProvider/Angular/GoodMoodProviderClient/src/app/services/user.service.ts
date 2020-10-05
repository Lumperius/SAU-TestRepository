import { RegistrationRequest } from './../classes/registrationRequest';
import { LoginRequest } from './../classes/loginRequest';
import { Observable, of} from 'rxjs';
import { User } from './../classes/user';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { catchError, map, tap } from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class UserService {

  loginUrl = 'https://localhost:44336/api/user/login/';
  registerUrl = 'https://localhost:44336/api/user/register/';
  curretUser: User;

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
    catchError(this.handleError<User>('loginUser'))
    ));
}


 private handleError<T>(operation = 'operation', result?: T) {
   return (error: any): Observable<T> => {
     console.error(error);
     return of(result as T);
    };
  }
}
