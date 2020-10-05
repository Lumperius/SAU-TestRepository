import { OfficeComponent } from './../office/office.component';
import { LoginRequest } from './../classes/loginRequest';
import { UserService } from './../services/user.service';
import { Component, OnInit } from '@angular/core';

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

  constructor(private userService: UserService) { }

  sendLogin(login: string, password: string): void{
    const request: LoginRequest = {
    Login: this.login,
    Password: this.password,
    };
    const user = this.userService.loginUser(request);
    if (user !== undefined){
      this.loginState = true;
      this.errorMessage = undefined;

    }
    else {
      this.loginState = false;
      this.errorMessage = 'Неправильный пароль или логин';
    }
   }

  ngOnInit(): void {
  }

}
