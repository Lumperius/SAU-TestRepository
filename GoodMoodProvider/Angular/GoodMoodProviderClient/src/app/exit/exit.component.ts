import { Router } from '@angular/router';
import { AuthenticationService } from './../services/authentication.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-exit',
  templateUrl: './exit.component.html',
  styleUrls: ['./exit.component.css']
})
export class ExitComponent implements OnInit {

  constructor(private router: Router) { }

  deauthorize(){
    localStorage.removeItem('id');
    localStorage.removeItem('token');
    this.router.navigate(['news']);
  }

  ngOnInit(): void {
  }

}
