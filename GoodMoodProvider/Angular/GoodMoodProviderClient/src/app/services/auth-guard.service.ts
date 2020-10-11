import { UserService } from './user.service';
import { AuthenticationService } from './authentication.service';
import { Injectable } from '@angular/core';
import { Router, CanActivate } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthGuardService {
  constructor(public authService: AuthenticationService, public router: Router,
              private userService: UserService) { }

  canActivate(): boolean {
    if (!this.authService.isAuthenticated()){
      if (!this.authService.refreshToken){
     this.router.navigate(['login']);
     return false;
    }}
    return true;
  }

}
