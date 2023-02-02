import {Component, Input, OnInit} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Person} from "../../../models/person.model";
import {ActivatedRoute, Router} from "@angular/router";
import {RestService} from "../../../services/rest.service";
import {Job} from "../../../models/job.model";
import {Company} from "../../../models/company.model";
import {ToastrService} from "ngx-toastr";

@Component({
  selector: 'app-overview',
  templateUrl: './overview.component.html',
  styleUrls: ['./overview.component.css']
})
export class OverviewComponent implements OnInit {
  @Input() currentPerson: Person = {
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
  @Input() isEditing: boolean = false;

  mothers: Person[] = []
  fathers: Person[] = []
  jobs: Job[] = [];
  companies: Company[] = [];

  constructor(private http: HttpClient,
              private activatedRoute: ActivatedRoute,
              private router: Router,
              private restService: RestService,
              private toastr: ToastrService) {
  }

  ngOnInit(): void {
    // get Parents for drop-downs
    this.getParents();

    // get Jobs & Companies for drop-downs
    this.getCompanyAndJobs()
  }

  onSubmit() {

    if (this.currentPerson.id != '') {
      this.restService.updatePerson(this.currentPerson).subscribe({
        next: d => {
          this.toastr.success("Successfully updated person (" + this.currentPerson.firstname + " " + this.currentPerson.lastname + ")!")
          if (this.router.url.split("/")[2] == "edit") {
            this.router.navigate(["/relations"]);
          } else {
            this.router.navigate(["persons"]);
          }
        },
        error: (err) => {
          this.toastr.error("Couldn't update person (" + this.currentPerson.firstname + " " + this.currentPerson.lastname + ")!")
        }
      })
    } else {
      this.restService.addPerson(this.currentPerson).subscribe({
        next: d => {
          this.router.navigate(["persons"]);
          this.toastr.success("Successfully added person (" + this.currentPerson.firstname + " " + this.currentPerson.lastname + ")!");
        },
        error: err => {
          this.toastr.error("Couldn't add person (" + this.currentPerson.firstname + " " + this.currentPerson.lastname + ")!");
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
    let index = this.mothers.map(x => x.id).indexOf(this.currentPerson.id);
    if (index > -1) {
      this.mothers.splice(index, 1);
    }

    //Fathers
    index = this.fathers.map(x => x.id).indexOf(this.currentPerson.id);
    if (index > -1) {
      this.fathers.splice(index, 1);
    }
  }

  delete() {
    if(this.currentPerson.id == null){
      this.toastr.error("Couldn't delete Person!");
      return;
    }

    this.restService.deletePerson(this.currentPerson.id).subscribe({
      next: v => {
        this.toastr.success("Successfully deleted person (" + this.currentPerson.firstname + " " + this.currentPerson.lastname + ")!")
        this.router.navigate(["persons"]);
      }
    })
  }
}

