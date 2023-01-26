import {Component, OnInit} from '@angular/core';
import {Person} from "../../models/person.model";
import {HttpClient} from '@angular/common/http';
import {environment} from '../../../environments/environment';

@Component({
  selector: 'app-person-list',
  templateUrl: './person-list.component.html',
  styleUrls: ['./person-list.component.css']
})
export class PersonListComponent implements OnInit {
  personList: Person[] = [];
  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.http.get<Person[]>(environment.API_URL + "person").subscribe({ next: value => { this.personList = value}})
  }

  getMotherString(person: Person) {
    return person.motherId == null ? "Unknown" : person.motherId;
  }
  getFatherString(person: Person) {
    return person.motherId == null ? "Unknown" : person.motherId;
  }
}



