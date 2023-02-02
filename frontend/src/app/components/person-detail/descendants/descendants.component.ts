import {Component, HostListener, OnInit} from '@angular/core';
import {Edge, Layout, Node} from '@swimlane/ngx-graph';
import * as shape from 'd3-shape';
import {ActivatedRoute, Router} from "@angular/router";
import {DagreNodesOnlyLayout} from "../../all-relations/customDagreNodesOnly";
import {RestService} from "../../../services/rest.service";

@Component({
  selector: 'app-descendants',
  templateUrl: './descendants.component.html',
  styleUrls: ['./descendants.component.css']
})
export class DescendantsComponent implements OnInit {
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
              private restService: RestService,
              private activatedRoute: ActivatedRoute) {
  }

  @HostListener('window:resize', ['$event'])
  getScreenSize(event?: any) {
    this.view = [window.innerWidth * 10 / 12, window.innerHeight * 10 / 12];
  }


  ngOnInit(): void {
    this.activatedRoute.params.subscribe(params => {
        if (params["id"]) {
          this.getData(params["id"])
        }
      },
    );
  }

  getData(id: string) {


    this.restService.getDescendants(id).subscribe(people => {
      const peopleIds = people.map(p=> p.id)
      people.forEach(p => {
        let childNode = p.id!;
        let fatherNode = p.fatherId!;
        let motherNode = p.motherId!;
        let clusterNode = motherNode + fatherNode;

        this.nodes.push({id: childNode, label: p.firstname + " " + p.lastname, data: {sex: p.sex}})

        if((p.fatherId != null && peopleIds.includes(p.fatherId))
          ||(p.motherId != null && peopleIds.includes(p.motherId))){
          if (!this.nodes.map(n => n.id).includes(clusterNode)){
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
        this.getScreenSize();
      })
    })
  }

  onNodeClick($event: any, node: any) {
    this.router.navigate(['/relations/edit', node.id]);
  }
}

