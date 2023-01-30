import {Component, OnInit} from '@angular/core';
import {Person} from "../../models/person.model";
import {HttpClient} from '@angular/common/http';
import {environment} from '../../../environments/environment';
import {RestService} from "../../services/rest.service";
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

  constructor(private http: HttpClient,
              private restService: RestService) { }

  ngOnInit(): void {
    this.getAllPeople();
  }
  getAllPeople(){
    this.restService.getAllPeople().subscribe({next: value => {
        this.personList = value
        this.personList.forEach(p => {
          if(p.fatherId){
            this.restService.getPerson(p.fatherId).subscribe({ next: value => { this.mother[p.id!] = value}})
          }
          else{
            this.father[p.id!] = null
          }
          if(p.motherId){
            this.restService.getPerson(p.motherId).subscribe({ next: value => { this.father[p.id!] = value}})
          }
          else{
            this.mother[p.id!] = null
          }
        })
      }})
  }

  deletePerson(person:Person) {
    if (person == null || person.id == null) return;

    this.restService.deletePerson(person.id).subscribe({
      next: d=> {
        const index = this.personList.indexOf(person, 0);
        if (index > -1) {
          this.personList.splice(index, 1);
        }
      }
    })
  }
}



