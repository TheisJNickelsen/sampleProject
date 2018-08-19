import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule }  from '@angular/forms';
import { SharedModule } from "../shared/shared.module";
 

import { EmailValidator } from '../directives/email.validator.directive';

import { routing }  from './account.routing';
import { RegistrationFormComponent } from './registration-form/registration-form.component';
import { LoginFormComponent } from './login-form/login-form.component';
import { UserService } from "../shared/services/user-service/user.service";



@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    routing,
    SharedModule
  ],
  declarations: [
    RegistrationFormComponent,
    EmailValidator,
    LoginFormComponent],
  providers:    [ UserService ]
})
export class AccountModule { }
