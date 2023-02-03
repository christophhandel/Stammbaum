import { Component, OnInit } from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {RestService} from "../../services/rest.service";

@Component({
  selector: 'app-stats',
  templateUrl: './stats.component.html',
  styleUrls: ['./stats.component.css']
})
export class StatsComponent implements OnInit {

  type: string = "company-chart";

  constructor(private activatedRoute: ActivatedRoute,
              private restService: RestService) {
  }

  ngOnInit(): void {
    // get current Person if there is one
    this.activatedRoute.params.subscribe(params => {      },
    );
  }
}
