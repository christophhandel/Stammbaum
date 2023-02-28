import {Component, Input, OnInit} from '@angular/core';
import {RestService} from "../../../services/rest.service";
import {ToastrService} from "ngx-toastr";
import {Company} from "../../../models/company.model";
import {Accomplishments} from "../../../models/accomplishments.model";

@Component({
  selector: 'app-accomplishment-list',
  templateUrl: './accomplishment-list.component.html',
  styleUrls: ['./accomplishment-list.component.css']
})
export class AccomplishmentListComponent implements OnInit {

  @Input()
  personId: string|null = '';
  accomplishments: Accomplishments[] = [{description: 'hello world', time: '2022-01-20'}];

  loading: boolean = true;

  constructor(private restService: RestService,
              private toastr: ToastrService) { }

  ngOnInit(): void {
    if(!this.personId) return;
    this.restService.getAccomplishmentsByPerson(this.personId).subscribe({
      next: d=> {this.accomplishments = d; this.loading=false},
      error: err=>this.toastr.error(err)
    })
  }

  deleteAcc(accs: any) {

  }
}
