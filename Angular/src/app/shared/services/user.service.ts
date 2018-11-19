import { UserRegistration } from './../models/user.registration.interface';
import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import {ajax} from 'rxjs/ajax';
import { ConfigService } from '../utils/config.service';

import {BaseService} from './base.service';
import 'rxjs';
import { Observable, of } from 'rxjs';
import { BehaviorSubject } from 'rxjs';

// Add the RxJS Observable operators we need in this app.

import {map} from 'rxjs/operators';
import {catchError} from 'rxjs/operators';


@Injectable()

export class UserService extends BaseService {

  baseUrl = '';

  // Observable navItem source
  private _authNavStatusSource = new BehaviorSubject<boolean>(false);
  // Observable navItem stream
  authNavStatus$ = this._authNavStatusSource.asObservable();

  private loggedIn = false;

  constructor(private http: Http, private configService: ConfigService) {
    super();
    this.loggedIn = !!localStorage.getItem('auth_token');
    // ?? not sure if this the best way to broadcast the status but seems to resolve issue on page refresh where auth status is lost in
    // header component resulting in authed user nav links disappearing despite the fact user is still logged in
    this._authNavStatusSource.next(this.loggedIn);
    this.baseUrl = configService.getApiURI();
  }

    register(email: string, password: string, firstName: string, lastName: string, location: string): Observable<UserRegistration> {
    const body = JSON.stringify({ email, password, firstName, lastName, location });
    const headers = new Headers({ 'Content-Type': 'application/json' });
    const options = new RequestOptions({ headers: headers });

    return this.http.post(this.baseUrl + '/accounts', body, options)
    .pipe(map(res =>  res.json()),
    catchError(this.handleError));

  }

   login(userName, password) {
    const headers = new Headers();
    headers.append('Content-Type', 'application/json');

    return this.http
      .post(
      this.baseUrl + '/auth/login',
      JSON.stringify({ userName, password }), { headers }
      )
      .pipe(map(res => res.json()),
      map(res => {
        localStorage.setItem('auth_token', res.auth_token);
        this.loggedIn = true;
        this._authNavStatusSource.next(true);
        return true;
      }),
      catchError(this.handleError));
  }

  logout() {
    localStorage.removeItem('auth_token');
    this.loggedIn = false;
    this._authNavStatusSource.next(false);
  }

  isLoggedIn() {
    return this.loggedIn;
  }
}
