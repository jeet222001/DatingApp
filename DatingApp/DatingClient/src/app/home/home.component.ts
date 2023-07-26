import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  users: any;

  constructor(private http: HttpClient) {

  }

  ngOnInit(): void {
    this.getUsers();
  }
  RegisterMode = false;

  RegisterToggle() {
    this.RegisterMode = true;
  }

  getUsers() {
    this.http.get('https://localhost:44320/api/users').subscribe({
      next: res => this.users = res,
      error: error => console.log(error),
      complete: () => console.log('request completed')

    })
  }
  CancelRegister(event:boolean){
    this.RegisterMode=event
  }
}
