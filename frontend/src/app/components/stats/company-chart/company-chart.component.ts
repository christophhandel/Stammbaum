import {
  ChangeDetectionStrategy,
  ChangeDetectorRef,
  Component,
  NgModule,
  OnChanges,
  OnInit,
  SimpleChanges
} from '@angular/core';
import {Color, ScaleType} from "@swimlane/ngx-charts";
import {RestService} from "../../../services/rest.service";
import {ToastrService} from "ngx-toastr";
import {Subject} from "rxjs";

@Component({
  selector: 'app-company-chart',
  templateUrl: './company-chart.component.html',
  styleUrls: ['./company-chart.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class CompanyChartComponent implements OnInit, OnChanges {
  multi: { name: string, series: { name: string, value: number }[] }[] = []

  loaded = false;

  view: [number, number] = [1000, 400];

  // options
  showXAxis: boolean = true;
  showYAxis: boolean = true;
  gradient: boolean = true;
  showLegend: boolean = true;
  showXAxisLabel: boolean = true;
  xAxisLabel: string = 'Company';
  showYAxisLabel: boolean = true;
  yAxisLabel: string = 'People';
  legendTitle: string = 'Gender';

  colorScheme: Color = {
    domain: ['#D1495B', '#00798C'],
    group: ScaleType.Ordinal,
    selectable: true,
    name: 'Customer Usage',
  };

  constructor(private restService: RestService,
              private cdRef:ChangeDetectorRef,
              private toastrService: ToastrService) {
  }

  ngOnChanges(changes: SimpleChanges): void {
    this.loadData();
  }

  ngOnInit(): void {
    this.loadData();
  }

  private loadData() {

    this.restService.getAllCompanies().subscribe({
      next: value => {
        const arr: { name: string, series: { name: string, value: number }[] }[] = [];
        value.forEach(s => arr.push({
          name: s.companyName,
          series: [
            {
              name: "Female",
              value: s.femaleWorkers
            },
            {
              name: "Male",
              value: s.maleWorkers
            }
          ]
        }));
        this.multi = [...arr]
        this.loaded = true;
        this.cdRef.detectChanges();
      },
      error: () => {
        this.toastrService.error("Die Daten konnten nicht geladen werden!")
      }
    })
  }
}


