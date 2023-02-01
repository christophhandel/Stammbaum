import {Component, OnInit} from '@angular/core';
import {Person} from "../../models/person.model";
import {HttpClient} from '@angular/common/http';
import {RestService} from "../../services/rest.service";
import {ToastrService} from "ngx-toastr";
import {cardAnimation, fadeIn} from "../../utils/animations";

interface IDictionary {
  [index: string]: Person | null;
}
@Component({
  selector: 'app-person-list',
  templateUrl: './person-list.component.html',
  styleUrls: ['./person-list.component.css'],
  animations: [
    cardAnimation
  ]
})

export class PersonListComponent implements OnInit {
  personList: Person[] = [];
  mother = {} as IDictionary;
  father = {} as IDictionary;

  loading: boolean = true;

  constructor(private http: HttpClient,
              private restService: RestService,
              private toastr: ToastrService) {
  }

  ngOnInit(): void {
    this.getAllPeople();
  }

  getAllPeople() {
    this.restService.getAllPeople().subscribe({
      next: value => {
        this.loading = false;

        this.personList = value
        this.personList.forEach(p => {
          if (p.fatherId) {
            this.restService.getPerson(p.fatherId).subscribe({
              next: value => {
                this.mother[p.id!] = value
              }
            })
          } else {
            this.father[p.id!] = null
          }
          if (p.motherId) {
            this.restService.getPerson(p.motherId).subscribe({
              next: value => {
                this.father[p.id!] = value
              }
            })
          } else {
            this.mother[p.id!] = null
          }
        })
      },
      error: err => {
        this.loading = false;
      }
    })
  }

  getSexString(p: Person) {
    switch (p.sex) {
      case 'm':
      case 'male':
        return 'Male'
      case 'f':
      case 'female':
        return 'Female'
      case 'o':
      case 'other':
        return 'Other'
    }
    return "Unknown;"
  }

  deletePerson(person: Person) {
    if (person == null || person.id == null) return;

    this.restService.deletePerson(person.id).subscribe({
      next: d => {
        const index = this.personList.indexOf(person, 0);
        if (index > -1) {
          this.personList.splice(index, 1);
          this.toastr.success("Successfully deleted person (" + person.firstname + " " + person.lastname + ")!")
        } else {
          this.toastr.error("Couldn't delete person (" + person.firstname + " " + person.lastname + ")!")
        }
      },
      error: err => {
        this.toastr.error("Couldn't delete person (" + person.firstname + " " + person.lastname + ")!" + err)
      }
    })
  }
}



