import { Router } from '@angular/router';
import decode from 'jwt-decode';
import { UserService } from './../services/user.service';
import { User } from './../models/user';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  constructor(private userService: UserService, private router: Router) { }

  CurrentUser: User = {
    id: 'default',
    password: 'default',
    login: 'default',
    email: 'default',
    token: 'noneYet',
    roles: ['literally nobody']
  };

  getUserFromToken(): void{
    const tokenPayload = decode(localStorage.getItem('token'));
    this.CurrentUser.login = tokenPayload.sub;
    this.CurrentUser.email = tokenPayload.email;
  }

  saveChanges(): void{
    this.userService.putUser(this.CurrentUser);
    this.router.navigate(['news']);
  }

  ngOnInit(): void {
    this.getUserFromToken();
  }

}
