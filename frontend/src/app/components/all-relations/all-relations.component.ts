import {Component, HostListener, OnInit} from '@angular/core';
import {Layout, Edge, Node} from '@swimlane/ngx-graph';
import * as shape from 'd3-shape';
import {Router} from "@angular/router";
import { DagreNodesOnlyLayout } from './customDagreNodesOnly'
import {RestService} from "../../services/rest.service";

@Component({
  selector: 'app-all-relations',
  templateUrl: './all-relations.component.html',
  styleUrls: ['./all-relations.component.css']
})
export class AllRelationsComponent implements OnInit {
  view: [number, number] = [10, 10];
  curve = shape.curveBundle.beta(1);

  public layout: Layout = new DagreNodesOnlyLayout();
  public links: Edge[] = [];
  public nodes: Node[] = [];

  layoutSettings = {
    orientation: 'TB'
  };


  constructor(private router : Router,
              private restService: RestService) {
  }

  @HostListener('window:resize', ['$event'])
  getScreenSize(event?: any) {
    this.view = [window.innerWidth * 10 / 12, window.innerHeight * 10 /12];
  }


  ngOnInit(): void {
    //this.getScreenSize();
    this.getData();
  }

  getData() {

    this.restService.getAllPeople().subscribe(people => {
      people.forEach(p => {
        this.nodes.push({id: p.id!, label: p.firstname + " " +  p.lastname})

        if (p.fatherId != null){
          this.links.push({source: p.fatherId, target: p.id!, label: "Father"})
        }

        if (p.motherId != null){
          this.links.push({source: p.motherId, target: p.id!, label: "Mother"})
        }
        this.getScreenSize();
      })
    })
  }

  onNodeClick($event: any, node: any) {
    this.router.navigate(['/persons', node.id]);
  }
}
