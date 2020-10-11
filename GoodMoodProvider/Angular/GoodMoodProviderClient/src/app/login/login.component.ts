import { JwtHelperService } from '@auth0/angular-jwt';
import { User } from '../models/user';
import { Router } from '@angular/router';
import { LoginRequest } from '../models/loginRequest';
import { UserService } from './../services/user.service';
import { Component, OnInit } from '@angular/core';
import decode from 'jwt-decode';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  login: string;
  password: string;
  loginState = false;
  errorMessage: string;
  test: any;
  user: User;

  constructor(private userService: UserService, private router: Router) { }

  sendLogin(login: string, password: string): void{
    const request: LoginRequest = {
    login: this.login,
    password: this.password,
    };
    this.userService.loginUser(request)
    .subscribe( response => {
      this.user = response;
      if (this.user !== undefined){
        this.loginState = true;
        this.errorMessage = undefined;
        this.userService.saveTokenAndIdInLocalStorage(this.user);
        this.router.navigate(['news']);
      }
      else {
        this.loginState = false;
        this.errorMessage = 'Неправильный пароль или логин';
      }
    });
   }

  ngOnInit(): void {
  }

}
