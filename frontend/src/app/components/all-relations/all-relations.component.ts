import {Component, HostListener, OnInit} from '@angular/core';
import {Edge, Layout, Node} from '@swimlane/ngx-graph';
import * as shape from 'd3-shape';
import {Router} from "@angular/router";
import {DagreNodesOnlyLayout} from './customDagreNodesOnly'
import {RestService} from "../../services/rest.service";

@Component({
  selector: 'app-all-relations',
  templateUrl: './all-relations.component.html',
  styleUrls: ['./all-relations.component.css']
})
export class AllRelationsComponent implements OnInit {
  view: [number, number] = [10, 10];
  curve = shape.curveBundle.beta(1);
  //curve = shape.curveLinear;

  public layout: Layout = new DagreNodesOnlyLayout();
  public links: Edge[] = [];
  public nodes: Node[] = [];

  layoutSettings = {
    orientation: 'TB'
  };


  constructor(private router: Router,
              private restService: RestService) {
  }

  @HostListener('window:resize', ['$event'])
  getScreenSize(event?: any) {
    this.view = [window.innerWidth * 10 / 12, window.innerHeight * 10 / 12];
  }


  ngOnInit(): void {
    //this.getScreenSize();
    this.getData();
  }

  getData() {

    this.restService.getAllPeople().subscribe(people => {
      people.forEach(p => {
        let childNode = p.id!;
        let fatherNode = p.fatherId!;
        let motherNode = p.motherId!;
        let clusterNode = motherNode + fatherNode;

        // CHILD TO CLUSTER CONNECTION
        this.nodes.push({id: childNode, label: p.firstname + " " + p.lastname, data: {sex: p.sex}})

        if(p.fatherId != null ||p.motherId != null){
          this.nodes.push({id: clusterNode, label: ".", data: {sex: "cluster"}})
          this.links.push({source: clusterNode, target: childNode, label: "Cluster", data: {type: "cluster"}})

          if (p.fatherId != null) {
            this.links.push({source: fatherNode, target: clusterNode, label: "Father", data: {type: "father"}})
          }
          if (p.motherId != null) {
            this.links.push({source: motherNode, target: clusterNode, label: "Mother", data: {type: "mother"}})
          }
        }
        this.getScreenSize();
      })
    })
  }

  onNodeClick($event: any, node: any) {
    this.router.navigate(['/relations/edit', node.id]);
  }
}


/*
*
        let childNode = p.id!;
        let fatherNode = p.fatherId!;
        let motherNode = p.motherId!;
        let clusterNode = motherNode + fatherNode;

        // CHILD TO CLUSTER CONNECTION
        this.nodes.push({id: childNode, label: p.firstname + " " + p.lastname, data: {sex: p.sex}})

        if(p.fatherId != null ||p.motherId != null){
          this.nodes.push({id: clusterNode, label: ".", data: {sex: "cluster"}})
          this.links.push({source: clusterNode, target: childNode, label: "Cluster"})

          if (p.fatherId != null) {
            this.links.push({source: fatherNode, target: clusterNode, label: "Father"})
          }
          if (p.motherId != null) {
            this.links.push({source: motherNode, target: clusterNode, label: "Mother"})
          }
        }
* */
