import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule }        from '@angular/forms';

import { routing }  from './dashboard.routing';
import { RootComponent } from './root/root.component';

import { AuthGuard } from '../auth.guard';
import { SharedModule } from "../shared/shared.module";
import { SettingsComponent } from "./settings/settings.component";
import { FetchDataComponent } from "../fetch-data/fetch-data.component";
import { SomeDataComponent } from "./some-data/some-data.component";
import { SomeDataDetailsComponent } from "./some-data/some-data-details/some-data-details.component";
import { SomeDataService } from "./some-data/services/some-data.service";
import { HomeComponent } from "./home/home.component";
import { GrowlModule } from 'primeng/primeng';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { StreamService } from './some-data/services/stream.service';
import { UserSearchResultComponent } from './some-data/user-search-result/user-search-result.component';
import { UserSearchBoxComponentComponent } from './some-data/user-search-box-component/user-search-box-component.component';


@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    routing,
    SharedModule,
    GrowlModule,
    NgbModule.forRoot()
  ],
  declarations: [
    RootComponent,
    SettingsComponent,
    FetchDataComponent,
    SomeDataComponent,
    SomeDataDetailsComponent,
    UserSearchResultComponent,
    UserSearchBoxComponentComponent],
  exports:      [ ],
  providers: [AuthGuard, SomeDataService, StreamService]
})
export class DashboardModule { }
