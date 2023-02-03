import {ChangeDetectionStrategy, Component, NgModule, OnInit} from '@angular/core';
import {multi, multi as multiA} from './data';
import {Color, ScaleType} from "@swimlane/ngx-charts";

@Component({
  selector: 'app-company-chart',
  templateUrl: './company-chart.component.html',
  styleUrls: ['./company-chart.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class CompanyChartComponent implements OnInit{
  multi = multiA;
  view: [number,number] = [700, 400];

  // options
  showXAxis: boolean = true;
  showYAxis: boolean = true;
  gradient: boolean = true;
  showLegend: boolean = true;
  showXAxisLabel: boolean = true;
  xAxisLabel: string = 'Country';
  showYAxisLabel: boolean = true;
  yAxisLabel: string = 'Population';
  legendTitle: string = 'Years';

  colorScheme: Color = {
    domain: ['#D1495B', '#00798C'],
    group: ScaleType.Ordinal,
    selectable: true,
    name: 'Customer Usage',
  };

  constructor() {
  }

  ngOnInit(): void {
    this.multi = multiA
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


