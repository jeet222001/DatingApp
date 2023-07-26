import { NotExpr } from '@angular/compiler';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AccountService } from 'src/_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter();
  model: any = {};
  constructor(private accountService: AccountService) { }
  ngOnInit(): void {
  }

  Register() {
    this.accountService.Register(this.model).subscribe(
      {
        next: response => console.log(response),
        error: error => console.log(error)

      }
    )
  }
  Cancel() {
    this.cancelRegister.emit(false);
  }
}
