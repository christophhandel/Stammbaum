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
  selector: 'app-company-add',
  templateUrl: './company-add.component.html',
  styleUrls: ['./company-add.component.css']
})
export class CompanyAddComponent implements OnInit {

  isEditing: boolean = true;
  model: Company = {
    id: '',
    name: '',
    businessActivity: ''
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
  }

  onSubmit() {
    console.log(this.model)

    if(this.model.id != ''){
      this.restService.updateCompany(this.model).subscribe({
        next: d => {
          this.router.navigate(["company"]);
          this.toastr.success("Successfully updated company (" + this.model.name + ")!")
        },
        error: err => {
          this.toastr.error("Couldn't update company (" + this.model.name + ")!")
        }
      })
    }
    else {
      this.restService.addCompany(this.model).subscribe({
        next: d => {
          this.router.navigate(["company"]);
          this.toastr.success("Successfully added company (" + this.model.name + ")!")
        },
        error: err => {
          this.toastr.error("Couldn't add company (" + this.model.name + ")!")
        }
      })
    }
    //TODO check if form is valid
  }
}
