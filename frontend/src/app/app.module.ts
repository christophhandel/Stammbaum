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

@NgModule({
  declarations: [
    AppComponent,
    PersonListComponent,
    AllRelationsComponent,
    PersonDetailComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    NgxGraphModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
