import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {AllRelationsComponent} from "./components/all-relations/all-relations.component";
import {PersonListComponent} from "./components/person-list/person-list.component";
import {PersonDetailComponent} from "./components/person-detail/person-detail.component";

const routes: Routes = [
  {path: 'relations', component: AllRelationsComponent},
  {path: 'person-detail', component: PersonDetailComponent},
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
