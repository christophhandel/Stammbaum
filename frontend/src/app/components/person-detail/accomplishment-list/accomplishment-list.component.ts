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
  accomplishments: Accomplishments[] = [];

  loading: boolean = true;

  constructor(private restService: RestService,
              private toastr: ToastrService) { }

  ngOnInit(): void {
  }

  deleteAcc(accs: any) {

  }
}
