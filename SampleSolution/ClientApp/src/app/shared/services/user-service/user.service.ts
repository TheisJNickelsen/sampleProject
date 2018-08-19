import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';

import { UserRegistration } from "../../models/user.registration.interface";

import { BaseService } from "../base.service";

import { Observable } from 'rxjs/Observable';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';

// Add the RxJS Observable operators we need in this app.
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/finally';
import { JwtTokenResponse } from "../../models/jwt.token.response";
import { Credentials } from "../../models/credentials.interface";
import { CredentialsDto } from "../../models/credentialsDto";
import { FacebookAuthDto } from "../../models/facebookAuthDto";
import { AuthService } from 'angular4-social-login';

@Injectable()
export class UserService extends BaseService {

  // Observable navItem source
  private _authNavStatusSource = new BehaviorSubject<boolean>(false);
  // Observable navItem stream
  authNavStatus$ = this._authNavStatusSource.asObservable();

  private loggedIn = false;

  constructor(private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string,
    private authService: AuthService) {
    super();
    this.loggedIn = !!localStorage.getItem('auth_token');
    this._authNavStatusSource.next(this.loggedIn);
  }

  register(email: string, password: string, firstName: string, lastName: string, location: string): Observable<UserRegistration> {
    let body = JSON.stringify({ email, password, firstName, lastName, location });
    let headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    let options = { headers: headers };

    return this.http.post(this.baseUrl + "api/accounts", body, options)
      .catch(this.handleError);
  }

  login(userName, password) {
    let headers = new HttpHeaders();
    headers.append('Content-Type', 'application/json');

    let credentials: CredentialsDto =
      {
        userName: userName,
        password: password
      };

    return this.http
      .post(this.baseUrl + 'api/auth/login', credentials, { headers })
      .map((res: JwtTokenResponse) => {
        localStorage.setItem('auth_token', res.auth_token);
        this.loggedIn = true;
        this._authNavStatusSource.next(true);
        return true;
      })
      .catch(this.handleError);
  }

  logout() {
    localStorage.removeItem('auth_token');
    this.loggedIn = false;
    this._authNavStatusSource.next(false);
    this.authService.signOut();
  }

  isLoggedIn() {
    return this.loggedIn;
  }

  facebookLogin(accessToken: string) {
    let headers = new HttpHeaders();
    headers.append('Content-Type', 'application/json');
    let body = new FacebookAuthDto();
    body.accessToken = accessToken;
    return this.http
      .post(this.baseUrl + 'api/externalauth/facebook', body, { headers })
      .map((res: JwtTokenResponse) => {
        localStorage.setItem('auth_token', res.auth_token);
        this.loggedIn = true;
        this._authNavStatusSource.next(true);
        return true;
      })
      .catch(this.handleError);
  }
}

