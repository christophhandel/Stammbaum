import {Component, OnInit} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {ActivatedRoute, Router} from "@angular/router";
import {RestService} from "../../services/rest.service";
import {Job} from "../../models/job.model";

@Component({
  selector: 'app-job-add',
  templateUrl: './job-add.component.html',
  styleUrls: ['./job-add.component.css']
})
export class JobAddComponent implements OnInit {

  isEditing: boolean = true;
  model: Job = {
    id: '',
    name: '',
    description: '',
  };

  jobs: Job[] = [];

  constructor(private http: HttpClient,
              private activatedRoute: ActivatedRoute,
              private router: Router,
              private restService: RestService) {
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
      this.restService.updateJob(this.model).subscribe({
        next: d => {
          this.router.navigate(["jobs"]);
        }
      })
    }
    else {
      this.restService.addJob(this.model).subscribe({
        next: d => {
          this.router.navigate(["jobs"]);
        }
      })
    }
    //TODO check if form is valid
  }
}
