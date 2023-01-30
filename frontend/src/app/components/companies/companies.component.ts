import { Component, OnInit } from '@angular/core';
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
    this.companyList.push({id: "löadfk", name: "IBM", businessActivity: "lösdkfjöldsajaöslsdfjlakdsfj"})
    this.companyList.push({id: "löadfk", name: "IBM", businessActivity: "lösdkfjöldsajaöslsdfjlakdsfj"})
    this.companyList.push({id: "löadfk", name: "IBM", businessActivity: "lösdkfjöldsajaöslsdfjlakdsfj"})
    this.companyList.push({id: "löadfk", name: "IBM", businessActivity: "lösdkfjöldsajaöslsdfjlakdsfj"})
    this.companyList.push({id: "löadfk", name: "IBM", businessActivity: "lösdkfjöldsajaöslsdfjlakdsfj"})
    this.companyList.push({id: "löadfk", name: "IBM", businessActivity: "lösdkfjöldsajaöslsdfjlakdsfj"})
    this.companyList.push({id: "löadfk", name: "IBM", businessActivity: "lösdkfjöldsajaöslsdfjlakdsfj"})
    // TODO get Data via RestService
    this.getCompanys();
  }

  deleteCompany(company: Company) {
    // TODO delete company via RestService
  }

  private getCompanys() {
    this.restService.getCompanys()
      .subscribe({
        next: (value: Company[]) => {
          this.companyList = value;
        }
      })
  }
}
