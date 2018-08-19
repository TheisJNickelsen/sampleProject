import { Subscription } from 'rxjs/Subscription';
import { Component, OnInit,OnDestroy } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthService, SocialUser } from "angular4-social-login";
import { FacebookLoginProvider, GoogleLoginProvider } from "angular4-social-login";

import { Credentials } from '../../shared/models/credentials.interface';
import { UserService } from "../../shared/services/user-service/user.service";

@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.scss']
})

export class LoginFormComponent implements OnInit, OnDestroy {

  private subscription: Subscription;

  brandNew: boolean;
  failed: boolean;
  errors: string;
  isRequesting: boolean;
  submitted: boolean = false;
  credentials: Credentials = { email: '', password: '' };
  private user: SocialUser;


  constructor(private userService: UserService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private authService: AuthService) { }

    ngOnInit() {

    this.subscription = this.activatedRoute.queryParams.subscribe(
      (param: any) => {
         this.brandNew = param['brandNew'];   
         this.credentials.email = param['email'];         
      });

      this.authService.authState.subscribe((user) => {
        this.user = user;

        if (user) { 
          this.handleFbLoggedIn(user);
        }
      });
    }

   ngOnDestroy() {
    this.subscription.unsubscribe();
   }

  launchFbLogin() {
    this.authService.signIn(FacebookLoginProvider.PROVIDER_ID);
  }

  handleFbLoggedIn(user: SocialUser) {
    if (!user) {
      this.failed = true;
    }
    else {
      this.failed = false;
      this.isRequesting = true;

      this.userService.facebookLogin(user.authToken)
        .finally(() => this.isRequesting = false)
        .subscribe(
          result => {
            if (result) {
              this.router.navigate(['/dashboard/home']);
            }
          },
          error => {
            this.failed = true;
            this.errors = error;
          });
    }
  }

  login({ value, valid }: { value: Credentials, valid: boolean }) {
    this.submitted = true;
    this.isRequesting = true;
    this.errors='';
    if (valid) {
      this.userService.login(value.email, value.password)
        .finally(() => this.isRequesting = false)
        .subscribe(
          result => {
            if (result) {
              this.router.navigate(['dashboard']);
            }
          },
          error => this.errors = error);
    }
  }
}
