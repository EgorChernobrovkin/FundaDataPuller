import { Component, OnInit } from '@angular/core';
import { Top10HubService } from "../../service/top10-hub.service";

@Component({
  selector: 'app-shell',
  templateUrl: './shell.component.html',
  styleUrls: ['./shell.component.css']
})
export class ShellComponent implements OnInit {

  constructor(private readonly hubService: Top10HubService) { }

  ngOnInit() {
    this.hubService.startConnection();
  }
}
