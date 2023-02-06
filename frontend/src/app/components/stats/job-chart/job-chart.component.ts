import {Component, OnChanges, OnInit, SimpleChanges} from '@angular/core';

import {Color, ScaleType} from '@swimlane/ngx-charts';
import {RestService} from "../../../services/rest.service";
import {ToastrService} from "ngx-toastr";

@Component({
  selector: 'app-job-chart',
  templateUrl: './job-chart.component.html',
  styleUrls: ['./job-chart.component.css']
})
export class JobChartComponent implements OnInit, OnChanges {
  single: { name: string, value: number }[] = [];
  view: [number, number] = [900, 400];
  gradient: boolean = true;

  colorScheme: Color = {
    domain: ['#00798C', '#D1495B', '#003D5B', '#EDAE49'],
    group: ScaleType.Ordinal,
    selectable: true,
    name: 'Customer Usage',
  };

  constructor(private restService: RestService, private toastrService: ToastrService) {
  }

  ngOnChanges(changes: SimpleChanges): void {
        this.loadData();
    }
  ngOnInit(): void {
    this.loadData();
  }

  loadData(): void {
    this.restService.getAllJobs().subscribe({
      next: value =>
      {
        this.single = [];
        value.forEach(s => this.single.push({
          name: s.jobName,
          value: s.maleWorkers+s.femaleWorkers
        }))
        console.log(this.single)
        this.single = [...this.single]
      },
      error:()=>{
        this.toastrService.error("Die Daten konnten nicht geladen werden!")
      }
    })
  }

  onSelect(data: any[]): void {
    console.log('Item clicked', JSON.parse(JSON.stringify(data)));
  }

  onActivate(data: any[]): void {
    console.log('Activate', JSON.parse(JSON.stringify(data)));
  }

  onDeactivate(data: any[]): void {
    console.log('Deactivate', JSON.parse(JSON.stringify(data)));
  }

}
