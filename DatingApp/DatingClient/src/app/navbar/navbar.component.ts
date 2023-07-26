import { Component, OnInit } from '@angular/core';
import { Observable, of } from 'rxjs';
import { User } from 'src/_models/user';
import { AccountService } from 'src/_services/account.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  model: any = {};
  constructor(public accountService: AccountService) {

  }
  ngOnInit(): void {
  }
  login() {
    this.accountService.login(this.model).subscribe({
      next: respons => {
        console.log(respons);
      },
      error: error => console.log(error)
    })
  }

  logout() {
    this.accountService.logout();
  }

}
