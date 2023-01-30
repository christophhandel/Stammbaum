import {environment} from '../../../environments/environment';
import {Component, OnInit} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Person} from "../../models/person.model";
import {ActivatedRoute, Router} from "@angular/router";
import {RestService} from "../../services/rest.service";
import {Job} from "../../models/job.model";
import {Company} from "../../models/company.model";
import {Location} from "../../models/location.model";

@Component({
  selector: 'app-person-detail',
  templateUrl: './person-detail.component.html',
  styleUrls: ['./person-detail.component.css']
})
export class PersonDetailComponent implements OnInit {

  isEditing: boolean = true;
  model : Person = {
    id: '',
    firstname: '',
    lastname: '',
    motherId: null,
    fatherId: null,
    sex: null,
    jobId: null,
    birthLocation: {id: null, city: "", country: ""},
    companyId: null
  };

  mothers: Person[] = []
  fathers: Person[] = []
  jobs: Job[] = [];
  companies: Company[] = [];
  locations: Location[] = [];

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

    // get Jobs & Companies for drop-downs
    this.getCompanyAndJobs()


    // get current Person if there is one
    this.activatedRoute.params.subscribe(params => {
      if (params["id"]) {
        this.restService.getPerson(params["id"]).subscribe(
          p => {console.log(p);this.model = p}
        );
      }
    });
  }

  onSubmit() {
    console.log(this.model)

    this.restService.addPerson(this.model).subscribe({
      next: d => {
        console.log(d)
      }
    })
    this.restService.updatePerson(this.model).subscribe({
      next: d => {
        console.log(d)
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

  getCompanyAndJobs() {
    this.restService.getCompanies().subscribe({
      next: value => {
        this.companies=value
      }
    })
    this.restService.getJobs().subscribe({
      next: value => {
        this.jobs=value
      }})
  }
}
