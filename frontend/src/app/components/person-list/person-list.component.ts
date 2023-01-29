import {Component, OnInit} from '@angular/core';
import {Person} from "../../models/person.model";
import {HttpClient} from '@angular/common/http';
import {environment} from '../../../environments/environment';
interface IDictionary {
  [index: string]: Person | null;
}
@Component({
  selector: 'app-person-list',
  templateUrl: './person-list.component.html',
  styleUrls: ['./person-list.component.css']
})

export class PersonListComponent implements OnInit {
  personList: Person[] = [];
  mother= {} as IDictionary;
  father={}as IDictionary;

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    //TODO use restService
    this.http.get<Person[]>(environment.API_URL + "person").subscribe({ next: value => {
      this.personList = value
      this.personList.forEach(p => {
        if(p.fatherId){
          this.getFatherString(p).subscribe({ next: value => { this.mother[p.id!] = value}})
        }
        else{
          this.father[p.id!] = null
        }
        if(p.motherId){
          this.getMotherString(p).subscribe({ next: value => { this.father[p.id!] = value}})
        }
        else{
          this.mother[p.id!] = null
        }
      })
    }})
  }

  getMotherString(person: Person) {
    //TODO use restService
     return this.http.get<Person>(environment.API_URL + "person/"+person.motherId);
  }
  getFatherString(person: Person) {
    //TODO use restService
    return this.http.get<Person>(environment.API_URL + "person/"+person.fatherId);
  }

  deletePerson(person:Person) {
    this.http.delete<Person>(environment.API_URL + "person/"+person.id).subscribe({
      next: d=> {
        const index = this.personList.indexOf(person, 0);
        if (index > -1) {
          this.personList.splice(index, 1);
        }
      }
    })
  }
}



