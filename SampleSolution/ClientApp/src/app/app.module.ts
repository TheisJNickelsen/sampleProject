import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { DashboardModule } from "./dashboard/dashboard.module";
import { AccountModule } from "./account/account.module";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { MessageService } from './shared/services/message-service/message.service';
import { UserService } from "./shared/services/user-service/user.service";
import { HomeComponent } from "./dashboard/home/home.component";

import { SocialLoginModule, AuthServiceConfig } from "angular4-social-login";
import { FacebookLoginProvider } from "angular4-social-login";
import { AngularFontAwesomeModule } from 'angular-font-awesome';
import { GrowlModule } from 'primeng/primeng';

let config = new AuthServiceConfig([
  {
    id: FacebookLoginProvider.PROVIDER_ID,
    provider: new FacebookLoginProvider("1720683821313599")
  }
]);

export function provideConfig() {
  return config;
}

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    AccountModule,
    DashboardModule,
    HttpClientModule,
    FormsModule,
    BrowserAnimationsModule,
    AngularFontAwesomeModule,
    GrowlModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' }
    ]),
    SocialLoginModule
  ],
  providers: [MessageService, UserService, {
    provide: AuthServiceConfig,
    useFactory: provideConfig
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
