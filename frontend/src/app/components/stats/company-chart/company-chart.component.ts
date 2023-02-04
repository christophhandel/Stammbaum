import {ChangeDetectionStrategy, Component, NgModule, OnInit} from '@angular/core';
import {multi, multi as multiA} from './data';
import {Color, ScaleType} from "@swimlane/ngx-charts";
import {RestService} from "../../../services/rest.service";
import {ToastrService} from "ngx-toastr";

@Component({
  selector: 'app-company-chart',
  templateUrl: './company-chart.component.html',
  styleUrls: ['./company-chart.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class CompanyChartComponent implements OnInit{
  multi: {name: string,series: {name:string,value:number}[]}[] = []

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

  constructor(private restService: RestService,private  toastrService: ToastrService) {
  }

  ngOnInit(): void {
    this.restService.getAllCompanies().subscribe({
      next: value =>
      {
        value.forEach(s => this.multi.push({
          name: s.companyName,
          series: [
            {
              name: "female",
              value: s.femaleWorkers
            },
            {
              name: "male",
              value: s.maleWorkers
            }
          ]
        }))
        console.log(this.multi)
        this.multi = [...this.multi]
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


