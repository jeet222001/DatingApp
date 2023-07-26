import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, map } from 'rxjs';
import { User } from 'src/_models/user';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  baseUrl = 'https://localhost:44320/api/';
  private currentUserSource = new BehaviorSubject<User | null>(null);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient) { }

  login(model: any) {
    return this.http.post<User>(this.baseUrl + 'account/login', model)
      .pipe(map(((response: User) => {
        const user = response;
        if (user) {
          localStorage.setItem('User', JSON.stringify(user));
          this.currentUserSource.next(user);
        }
      })));
  }
  setCurrentUser(user: User) {
    this.currentUserSource.next(user);

  }
  logout() {
    localStorage.removeItem('User');
    this.currentUserSource.next(null);
  }

  Register(model: any) {
    return this.http.post<User>(this.baseUrl + 'account/register', model).pipe(
      map(
        (user: User) => {
          if (user) {
            localStorage.setItem('User', JSON.stringify(user));
          }
          return user
        }
      )
    )
  }
}
