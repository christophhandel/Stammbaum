import {Component, OnInit} from '@angular/core';
import {Job} from "../../models/job.model";
import {RestService} from "../../services/rest.service";
import {ToastrService} from "ngx-toastr";

@Component({
  selector: 'app-jobs',
  templateUrl: './jobs.component.html',
  styleUrls: ['./jobs.component.css']
})
export class JobsComponent implements OnInit {
  jobs: Job[] = [];

  constructor(private restService: RestService,
              private toastr: ToastrService) { }

  ngOnInit(): void {

    this.restService.getJobs().subscribe({
      next: value => {
        this.jobs=value;
      }
    })
  }

  deleteJob(job: Job) {
    if (job == null || job.id == null) return;

    return this.restService.deleteJob(job.id).subscribe({
      next: d=> {
        const index = this.jobs.indexOf(job, 0);
        if (index > -1) {
          this.jobs.splice(index, 1);
          this.toastr.success("Successfully deleted job (" + job.name + ")!")
        }
        else {
          this.toastr.error("Couldn't delete job (" + job.name + ")!")
        }
      },
      error: err => {
        this.toastr.error("Couldn't delete job (" + job.name + ")!" + err)
      }
    })
  }
}
