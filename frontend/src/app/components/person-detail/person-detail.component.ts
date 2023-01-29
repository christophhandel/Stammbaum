import {environment} from '../../../environments/environment';
import {Component, OnInit} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Person} from "../../models/person.model";
import {ActivatedRoute} from "@angular/router";
import {Router} from "@angular/router";
import {RestService} from "../../services/rest.service";

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
              private activatedRoute: ActivatedRoute,
              private router: Router,
              private restService: RestService) {
  }

  ngOnInit(): void {
    console.log(this.router.url.split("/"));

    if (this.router.url.split("/")[2] == "add") {
      this.isEditing = false;
    }

    // get Parents for drop-downs
    this.getParents();

    // get current Person if there is one
    this.activatedRoute.params.subscribe(params => {
      if (params["id"]) {
        this.restService.getPerson(params["id"]).subscribe(
          p => this.model = p
        );
      }
    });
  }

  onSubmit() {
    console.log(this.model)

    //TODO use restService
    this.http.post<Person>(environment.API_URL + "Person", this.model).subscribe({
      next: d => {
        if (this.model.id == '') {
          this.http.post<Person>(environment.API_URL + "Person", this.model).subscribe({
            next: d => {
              console.log(d)
            }
          })
        } else {
          this.http.put<Person>(environment.API_URL + "Person/" + this.model.id, this.model).subscribe({
            next: d => {
              console.log(d)
            }
          })
        }
      }
    })
  }

  getParents() {
    this.restService.getBySex("f")
      .subscribe({
        next: value => {
          this.mothers = value;
        }
      })

    this.restService.getBySex("m")
      .subscribe({
        next: value => {
          this.fathers = value;
        }
      })
  }
}
