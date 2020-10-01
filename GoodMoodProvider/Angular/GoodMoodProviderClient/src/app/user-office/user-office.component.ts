import { User } from '../classes/user';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-user-office',
  templateUrl: './user-office.component.html',
  styleUrls: ['./user-office.component.css']
})
export class UserOfficeComponent implements OnInit {

  currentUser: User;

  constructor() { }

  ngOnInit(): void {
  }

}
