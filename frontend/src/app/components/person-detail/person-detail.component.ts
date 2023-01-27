import {environment} from '../../../environments/environment';
import {Component, OnInit} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Person} from "../../models/person.model";
import {Router} from "@angular/router";

@Component({
  selector: 'app-person-detail',
  templateUrl: './person-detail.component.html',
  styleUrls: ['./person-detail.component.css']
})
export class PersonDetailComponent implements OnInit {

  isEditing: boolean = true;
  model: Person = {id: '', firstname: '', lastname: "", motherId: null, fatherId: null, sex: null};

  mothers: Person[] = []
  fathers: Person[] = []

  constructor(private http: HttpClient,
              private router: Router) {
  }

  ngOnInit(): void {
    console.log(this.router.url.split("/"));

    if (this.router.url.split("/")[2] == "add") {
      this.isEditing = false;
    }

    this.http.get<Person[]>(environment.API_URL + "person?sex=f").subscribe({
      next: d => {
        this.mothers = d;
      }
    })
    this.http.get<Person[]>(environment.API_URL + "person?sex=m").subscribe({
      next: d => {
        this.fathers = d;
      }
    })
  }


  onSubmit() {
    console.log(this.model)
    this.http.post<Person>(environment.API_URL + "Person", this.model).subscribe({
      next: d => {
        console.log(d)
      }
    })
  }
}
