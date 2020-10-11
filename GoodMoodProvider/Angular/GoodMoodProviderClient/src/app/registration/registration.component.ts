import { User } from '../models/user';
import { Router } from '@angular/router';
import { RegistrationRequest } from '../models/registrationRequest';
import { UserService } from './../services/user.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {

  login: string;
  password: string;
  confirmPassword: string;
  email: string;
  registrationState: boolean;
  errorMessage: string;

  constructor(private userService: UserService, private router: Router) { }

  register(): void{
    if (this.password === this.confirmPassword){
      const request: RegistrationRequest = {
        login: this.login,
        password: this.password,
        email: this.email,
      };
      let user: User;
      this.userService.registerUser(request)
        .subscribe( response => user = response);
      this.userService.saveTokenAndIdInLocalStorage(user);
    }
    else {
      this.registrationState = false;
      this.errorMessage = 'Пароли не совпадают';
    }
   }

  ngOnInit(): void {
  }

}
