import { ActivatedRouteSnapshot, Router, CanActivate } from '@angular/router';
import { AuthenticationService } from './authentication.service';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class RoleGuardService {

  constructor(private authServide: AuthenticationService, private router: Router) { }

  canActivate(route: ActivatedRouteSnapshot): boolean {
   const expectedRole = route.data.expectedRole;
   const tokenPayload = this.authServide.getDecodedAccessToken(localStorage.getItem('token'));
   const roles: string[] = tokenPayload.role;
   if (!this.authServide.isAuthenticated() || roles.find(expectedRole) !== undefined){
     this.router.navigate(['login']);
     return false;
   }
   else{
   return true;
  }
  }

}
