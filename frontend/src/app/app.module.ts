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
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {JobsComponent} from './components/jobs/jobs.component';
import {CompaniesComponent} from './components/companies/companies.component';
import {CompanyAddComponent} from './components/company-add/company-add.component';
import {JobAddComponent} from './components/job-add/job-add.component';
import {ToastrModule} from "ngx-toastr";
import {SettingsComponent} from './components/settings/settings.component';
import {OverviewComponent} from './components/person-detail/overview/overview.component';
import {FamilyTreeComponent} from './components/family-tree/family-tree.component';
import {CompanyChartComponent} from "./components/stats/company-chart/company-chart.component";
import {NgxChartsModule} from "@swimlane/ngx-charts";
import {StatsComponent} from "./components/stats/stats.component";
import {JobChartComponent} from "./components/stats/job-chart/job-chart.component";


@NgModule({
  declarations: [
    AppComponent,
    PersonListComponent,
    AllRelationsComponent,
    PersonDetailComponent,
    JobsComponent,
    StatsComponent,
    CompanyChartComponent,
    JobChartComponent,
    CompaniesComponent,
    CompanyAddComponent,
    JobAddComponent,
    SettingsComponent,
    OverviewComponent,
    FamilyTreeComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    NgxGraphModule,
    NgxChartsModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot()
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
