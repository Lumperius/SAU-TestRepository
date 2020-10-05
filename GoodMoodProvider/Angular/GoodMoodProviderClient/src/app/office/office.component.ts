import { UserService } from './../services/user.service';
import { User } from './../classes/user';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-office',
  templateUrl: './office.component.html',
  styleUrls: ['./office.component.css']
})
export class OfficeComponent implements OnInit {

  CurrentUser: User;

  constructor(private userService: UserService) { }

  loginUser(user: User): void{
  this.CurrentUser = user;
  }

  ngOnInit(): void {
  }

}
