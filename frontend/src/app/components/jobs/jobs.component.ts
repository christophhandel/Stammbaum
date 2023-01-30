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
    //TODO get all Jobs with restService and save to jobs variable
  }

  deleteJob(job: Job) {
    //TODO delete job with restService
  }
}
