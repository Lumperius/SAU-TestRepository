import { RegistrationRequest } from './../classes/registrationRequest';
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

  constructor(private userService: UserService) { }

  register(login: string, password: string, confirmPassword: string): void{
    if (password === confirmPassword){
      const request: RegistrationRequest = {
        Login: this.login,
        Password: this.password,
        Email: this.email,
      };
      const user = this.userService.registerUser(request);
      if (user !== undefined){
        this.registrationState = true;
        this.errorMessage = undefined;
      }
      else{
        this.registrationState = false;
        this.errorMessage = 'Регистрация не удалась';
      }
    }
    else {
      this.registrationState = false;
      this.errorMessage = 'Пароли не совпадают';
    }
   }

  ngOnInit(): void {
  }

}
