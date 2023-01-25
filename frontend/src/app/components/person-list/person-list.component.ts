import {Component, OnInit} from '@angular/core';
import {Person} from "../../models/person.model";

@Component({
  selector: 'app-person-list',
  templateUrl: './person-list.component.html',
  styleUrls: ['./person-list.component.css']
})
export class PersonListComponent implements OnInit {
  personList: Person[] = [];
  constructor() { }

  ngOnInit(): void {
    this.personList.push({firstname: "Ablinger", lastname: "Raphael", sex: "Male", fatherId: null, motherId: null, id: null});
    this.personList.push({firstname: "Saskia", lastname: "Humanbert", sex: "Female", fatherId: null, motherId: null, id: null});
    this.personList.push({firstname: "Bruno", lastname: "der Kameramann", sex: "Male", fatherId: null, motherId: null, id: null});
    this.personList.push({firstname: "Niklas", lastname: "Aichinger", sex: "Male", fatherId: null, motherId: null, id: null});
  }

  getMotherString(person: Person) {
    return person.motherId == null ? "Unknown" : person.motherId;
  }
  getFatherString(person: Person) {
    return person.motherId == null ? "Unknown" : person.motherId;
  }
}
