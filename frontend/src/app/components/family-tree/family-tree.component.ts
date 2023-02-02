import {Component, Input, OnChanges, OnInit, SimpleChanges} from '@angular/core';
import {Person} from "../../models/person.model";
import {Router} from "@angular/router";
import * as shape from "d3-shape";
import {Edge, Layout, Node} from "@swimlane/ngx-graph";
import {DagreNodesOnlyLayout} from "./customDagreNodesOnly";

@Component({
  selector: 'app-family-tree',
  templateUrl: './family-tree.component.html',
  styleUrls: ['./family-tree.component.css']
})
export class FamilyTreeComponent implements OnInit, OnChanges {
  @Input() people: Person[] = []
  @Input() view: [number, number] = [10, 10];
  curve = shape.curveBundle.beta(1);
  public layout: Layout = new DagreNodesOnlyLayout();
  public links: Edge[] = [];
  public nodes: Node[] = [];
  layoutSettings = {
    orientation: 'TB'
  };
  constructor(private router: Router) {
  }
  ngOnChanges(changes: SimpleChanges){
    this.convertData();
  }
  ngOnInit(): void {
    this.convertData()
  }
  onNodeClick($event: any, node: any) {
    this.router.navigate(['/relations/edit', node.id]);
  }
  convertData() {
    const peopleIds = this.people.map(p => p.id);
    this.nodes = [];
    this.links = []

    this.people.forEach(p => {
      let childNode = p.id!;
      let fatherNode = p.fatherId!;
      let motherNode = p.motherId!;
      let clusterNode = motherNode + fatherNode;

      this.nodes.push({id: childNode, label: p.firstname + " " + p.lastname, data: {sex: p.sex}})

      if ((p.fatherId != null && peopleIds.includes(p.fatherId)) ||
        (p.motherId != null && peopleIds.includes(p.motherId))
      ) {

        if (!this.nodes.map(n => n.id).includes(clusterNode)) {
          this.nodes.push({id: clusterNode, label: ".", data: {sex: "cluster"}})
        }


        this.links.push({source: clusterNode, target: childNode, label: "Cluster", data: {type: "cluster"}})

        if (p.fatherId != null && peopleIds.includes(p.fatherId)) {
          this.links.push({source: fatherNode, target: clusterNode, label: "Father", data: {type: "father"}})
        }
        if (p.motherId != null && peopleIds.includes(p.motherId)) {
          this.links.push({source: motherNode, target: clusterNode, label: "Mother", data: {type: "mother"}})
        }
      }
    });
  }
}
