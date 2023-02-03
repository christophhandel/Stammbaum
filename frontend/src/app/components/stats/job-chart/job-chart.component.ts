import {Component, NgModule, OnInit} from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { NgxChartsModule } from '@swimlane/ngx-charts';
import { single,single as singleA } from './data';

import { Color, ScaleType } from '@swimlane/ngx-charts';

@Component({
  selector: 'app-job-chart',
  templateUrl: './job-chart.component.html',
  styleUrls: ['./job-chart.component.css']
})
export class JobChartComponent implements OnInit{
  single= singleA;
  view: [number,number] = [900, 400];

  // options
  gradient: boolean = true;
  showLegend: boolean = true;
  showLabels: boolean = true;
  isDoughnut: boolean = false;

  colorScheme: Color = {
    domain: ['#00798C', '#D1495B','#003D5B','#EDAE49'],
    group: ScaleType.Ordinal,
    selectable: true,
    name: 'Customer Usage',
  };
  constructor() {
  }

  onSelect(data: any[]): void {
    console.log('Item clicked', JSON.parse(JSON.stringify(data)));
  }

  onActivate(data:any[]): void {
    console.log('Activate', JSON.parse(JSON.stringify(data)));
  }

  onDeactivate(data:any[]): void {
    console.log('Deactivate', JSON.parse(JSON.stringify(data)));
  }

  ngOnInit(): void {
    this.single =singleA
  }
}
