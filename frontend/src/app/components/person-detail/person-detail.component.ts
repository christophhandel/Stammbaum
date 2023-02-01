import {Component, OnInit} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Person} from "../../models/person.model";
import {ActivatedRoute, Router} from "@angular/router";
import {RestService} from "../../services/rest.service";
import {Job} from "../../models/job.model";
import {Company} from "../../models/company.model";
import {Location} from "../../models/location.model";
import {ToastrService} from "ngx-toastr";

@Component({
  selector: 'app-person-detail',
  templateUrl: './person-detail.component.html',
  styleUrls: ['./person-detail.component.css']
})
export class PersonDetailComponent implements OnInit {

  isEditing: boolean = true;
  model: Person = {
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
              private restService: RestService,
              private toastr: ToastrService) {
  }

  ngOnInit(): void {
    //console.log(this.router.url.split("/"));

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
            {
              next: (value) => {
                console.log(value);
                this.model = value
                this.removeCurrentFromParentList();
              },
              error: (err) => {
                this.toastr.error("Couldn't get person! " + err)
              }
            }
          );
        }
      },
    );
  }

  onSubmit() {
    console.log(this.model)

    if (this.model.id != '') {
      this.restService.updatePerson(this.model).subscribe({
        next: d => {
          this.toastr.success("Successfully updated person (" + this.model.firstname + " " + this.model.lastname + ")!")
          if (this.router.url.split("/")[2] == "edit") {
            this.router.navigate(["/relations"]);
          } else {
            this.router.navigate(["persons"]);
          }
        },
        error: (err) => {
          this.toastr.error("Couldn't update person (" + this.model.firstname + " " + this.model.lastname + ")!" + err)
        }
      })
    } else {
      this.restService.addPerson(this.model).subscribe({
        next: d => {
          this.router.navigate(["persons"]);
          this.toastr.success("Successfully added person (" + this.model.firstname + " " + this.model.lastname + ")!");
        },
        error: err => {
          this.toastr.error("Couldn't add person (" + this.model.firstname + " " + this.model.lastname + ")!" + err);
        }
      })
    }

    //TODO check if form is valid
  }

  getParents() {
    this.restService.getBySex("f")
      .subscribe({
        next: value => {
          this.mothers = value;
          this.removeCurrentFromParentList();
        }
      })

    this.restService.getBySex("m")
      .subscribe({
        next: value => {
          this.fathers = value;
          this.removeCurrentFromParentList();
        }
      })
  }

  getCompanyAndJobs() {
    this.restService.getCompanies().subscribe({
      next: value => {
        this.companies = value
      }
    })
    this.restService.getJobs().subscribe({
      next: value => {
        this.jobs = value
      }
    })
  }

  removeCurrentFromParentList() {
    //Mothers
    let index = this.mothers.map(x => x.id).indexOf(this.model.id);
    if (index > -1) {
      this.mothers.splice(index, 1);
    }

    //Fathers
    index = this.fathers.map(x => x.id).indexOf(this.model.id);
    if (index > -1) {
      this.fathers.splice(index, 1);
    }
  }
}
