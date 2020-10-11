import { UserService } from './user.service';
import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { decode } from 'jwt-decode';

@Injectable({
  providedIn: 'root'
})


export class AuthenticationService {

  constructor(private userService: UserService) { }

  public isAuthenticated(): boolean{
    const jwtHelper: JwtHelperService = new JwtHelperService();
    const token = localStorage.getItem('token');

    return !jwtHelper.isTokenExpired(token);
  }

  refreshToken(): boolean {
    let token;
    this.userService.httpRefreshToken().subscribe( response => token = response);
    if ( token === undefined ) {
      localStorage.setItem('wow', 'ie');
      return false;
    }
    else{
    localStorage.setItem('token', token);
    localStorage.setItem('ooo', 'ie');
    return true;
  }
   }

   getDecodedAccessToken(token: string): any {
    try{
        const jwtHelper = new JwtHelperService();
        return jwtHelper.decodeToken(token);
    }
    catch (Error){
        return null;
    }
  }
}
