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
    this.restService.getCompanies().subscribe({
      next: value => {
        this.companyList=value;
      }
    })
  }

  deleteCompany(company: Company) {
    if (company == null || company.id == null) return;

    return this.restService.deleteCompany(company.id).subscribe({
      next: d=> {
        const index = this.companyList.indexOf(company, 0);
        if (index > -1) {
          this.companyList.splice(index, 1);
        }
      }
    })
  }
}
