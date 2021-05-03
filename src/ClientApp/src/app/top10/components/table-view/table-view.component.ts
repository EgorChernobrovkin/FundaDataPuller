import { Component, Input, OnInit } from '@angular/core';
import { Top10Agents } from "../../models/top10-agents.model";

@Component({
  selector: 'app-top10-table-view',
  templateUrl: './table-view.component.html',
  styleUrls: ['./table-view.component.css']
})
export class TableViewComponent implements OnInit {

  constructor() { }

  @Input()
  public top10Agents: Top10Agents;

  ngOnInit() {
  }

}
