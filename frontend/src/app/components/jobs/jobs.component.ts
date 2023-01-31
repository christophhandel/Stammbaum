import {Component, OnInit} from '@angular/core';
import {Job} from "../../models/job.model";
import {RestService} from "../../services/rest.service";

@Component({
  selector: 'app-jobs',
  templateUrl: './jobs.component.html',
  styleUrls: ['./jobs.component.css']
})
export class JobsComponent implements OnInit {
  jobs: Job[] = [];

  constructor(private restService: RestService) { }

  ngOnInit(): void {
    // this.jobs.push({id: "löadfk", name: "Software Engineer", description: "0101001001000001"})
    // this.jobs.push({id: "löadfk", name: "Accountant", description: "An accountant is a practitioner of accounting or accountancy. Accountants who have demonstrated competency through their professional associations' certification exams are certified to use titles such as Chartered Accountant, Chartered Certified Accountant or Certified Public Accountant, or Registered Public Accountant"})
    // this.jobs.push({id: "löadfk", name: "Student", description: "Going to school and that kinda stuff"})
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
        }
      }
    })
  }
}
