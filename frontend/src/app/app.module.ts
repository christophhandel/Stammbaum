import {NgModule} from '@angular/core';
import {FormsModule} from '@angular/forms';
import {BrowserModule} from '@angular/platform-browser';
import {HttpClientModule} from '@angular/common/http';

import {AppRoutingModule} from './app-routing.module';
import {AppComponent} from './app.component';
import {PersonListComponent} from './components/person-list/person-list.component';
import {AllRelationsComponent} from './components/all-relations/all-relations.component';
import {PersonDetailComponent} from './components/person-detail/person-detail.component';
import {NgxGraphModule} from "@swimlane/ngx-graph";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { JobsComponent } from './components/jobs/jobs.component';
import { CompaniesComponent } from './components/companies/companies.component';
import { LocationsComponent } from './components/locations/locations.component';

@NgModule({
  declarations: [
    AppComponent,
    PersonListComponent,
    AllRelationsComponent,
    PersonDetailComponent,
    JobsComponent,
    CompaniesComponent,
    LocationsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    NgxGraphModule,
    BrowserAnimationsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
