import {Component, OnInit} from '@angular/core';
import {Company} from "../../models/company.model";
import {RestService} from "../../services/rest.service";

@Component({
  selector: 'app-companies',
  templateUrl: './companies.component.html',
  styleUrls: ['./companies.component.css']
})
export class CompaniesComponent implements OnInit {
  companyList: Company[] = [];

  constructor(private restService: RestService) { }

  ngOnInit(): void {
    // TODO get Data via RestService
    this.restService.getCompanies().subscribe({
      next: value => {
        this.companyList=value;
      }
    })
  }

  deleteCompany(company: Company) {
    // TODO delete company via RestService
  }
}
