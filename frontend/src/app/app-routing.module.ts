import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {AllRelationsComponent} from "./components/all-relations/all-relations.component";
import {PersonListComponent} from "./components/person-list/person-list.component";
import {PersonDetailComponent} from "./components/person-detail/person-detail.component";
import {JobsComponent} from "./components/jobs/jobs.component";
import {CompaniesComponent} from "./components/companies/companies.component";
import {LocationsComponent} from "./components/locations/locations.component";

const routes: Routes = [
  {path: 'relations', component: AllRelationsComponent},
  {path: 'jobs', component: JobsComponent},
  {path: 'company', component: CompaniesComponent},
  {path: 'locations', component: LocationsComponent},
  {path: 'people', component: PersonListComponent},
  {path: 'people/add', component: PersonDetailComponent},
  {path: 'people/:id', component: PersonDetailComponent},
  {path: '', redirectTo: 'relations', pathMatch: 'full'},
  {path: '**', component: AllRelationsComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
