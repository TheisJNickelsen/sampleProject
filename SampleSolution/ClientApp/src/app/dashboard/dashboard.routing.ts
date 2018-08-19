import { ModuleWithProviders } from '@angular/core';
import { RouterModule }        from '@angular/router';

import { RootComponent }    from './root/root.component';
import { HomeComponent }    from './home/home.component'; 
import { SettingsComponent }    from './settings/settings.component'; 

import { AuthGuard } from '../auth.guard';
import { FetchDataComponent } from "../fetch-data/fetch-data.component";
import { SomeDataComponent } from "./some-data/some-data.component";
import { SomeDataDetailsComponent } from "./some-data/some-data-details/some-data-details.component";

export const routing: ModuleWithProviders = RouterModule.forChild([
  {
      path: 'dashboard',
      component: RootComponent, canActivate: [AuthGuard],

      children: [      
       { path: '', component: HomeComponent },
       { path: 'home',  component: HomeComponent },
        { path: 'settings', component: SettingsComponent },
        { path: 'fetch-data', component: FetchDataComponent },
        { path: 'my-data', component: SomeDataComponent },
        { path: 'create-some-data', component: SomeDataDetailsComponent, data: { isEdit: false } },
        { path: 'edit-some-data', component: SomeDataDetailsComponent, data: { isEdit: true } }
      ]       
    }  
]);

