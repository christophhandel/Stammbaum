import {Component, HostListener, OnInit} from '@angular/core';
import {Layout, Edge, Node, DagreNodesOnlyLayout} from '@swimlane/ngx-graph';
import * as shape from 'd3-shape';
import {Router} from "@angular/router";

@Component({
  selector: 'app-all-relations',
  templateUrl: './all-relations.component.html',
  styleUrls: ['./all-relations.component.css']
})
export class AllRelationsComponent implements OnInit {
  view: [number, number] = [1000, 1000];
  curve = shape.curveBundle.beta(1);

  public layout: Layout = new DagreNodesOnlyLayout();
  public links: Edge[] = [];
  public nodes: Node[] = [];

  clusters: { childNodeIds: string[]; id: string; label: string }[] = [];
  edges: ({ id: string; source: string; target: string })[] = [];


  constructor(private router : Router) {
    this.getScreenSize();
    this.showGraph();
  }

  @HostListener('window:resize', ['$event'])
  getScreenSize(event?: any) {
    this.view = [window.innerWidth * 9 / 12, window.innerHeight];
  }


  ngOnInit(): void {
  }

  showGraph() {
    this.nodes = [
      {id: 'start', label: 'Root', position: {x: 0, y: 0}},
      {id: '1', label: 'Neidi', position: {x: 1, y: 0}},
      {id: '11', label: 'Nicole', position: {x: 1, y: 0}},
      {id: '2', label: 'Neidis Gschwister 1', position: {x: 2, y: 0}},
      {id: '3', label: 'Neidis Kind', position: {x: 3, y: 0}},
      {id: '4', label: 'Neidis Gschwisters 1 Kind 2', position: {x: 4, y: 0}},
      {id: '5', label: 'Neidis Kindes Kind', position: {x: 5, y: 0}},
      {id: '6', label: 'Neidis Gschwisters 1 Kind 1', position: {x: 6, y: 0}}
    ];

    this.links = [
      {source: 'start', target: '1', label: 'Parent'},
      {source: 'start', target: '2', label: 'Parent'},
      {source: '1', target: '3', label: 'Parent'},
      {source: '11', target: '3', label: 'Parent'},
      {source: '2', target: '4', label: 'Parent'},
      {source: '2', target: '6', label: 'Parent'},
      {source: '3', target: '5', label: 'Parent'},
    ];

  }

  onNodeClick($event: any, node: any) {
    this.router.navigate(['/person', node.id]);
  }
}
