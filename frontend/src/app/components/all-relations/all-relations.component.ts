import {Component, HostListener, OnInit} from '@angular/core';
import {RestService} from "../../services/rest.service";
import {Person} from "../../models/person.model";

@Component({
  selector: 'app-all-relations',
  templateUrl: './all-relations.component.html',
  styleUrls: ['./all-relations.component.css']
})
export class AllRelationsComponent implements OnInit {
  view: [number, number] = [10, 10];
  persons : Person[] = [];


  constructor(private restService: RestService) {
  }

  @HostListener('window:resize', ['$event'])
  getScreenSize(event?: any) {
    this.view = [window.innerWidth * 11 / 12, window.innerHeight * 10.56 / 12];
  }


  ngOnInit(): void {
    this.getData();
  }

  getData() {

    this.restService.getAllPeople().subscribe(people => {
      this.persons = people;
      this.getScreenSize();
    })
  }
}
