import { Component, OnDestroy, OnInit } from '@angular/core';
import { Top10HubService } from "../../service/top10-hub.service";
import { Top10DataService } from "../../service/top10-data.service";
import { BehaviorSubject, Observable, Subject } from "rxjs";
import { HubMethods } from "../../models/hub-methods";
import { Top10Agents } from "../../models/top10-agents.model";
import { takeUntil } from "rxjs/operators";
import { MatSnackBar } from "@angular/material/snack-bar";

@Component({
  selector: 'app-top10-by-garden',
  templateUrl: './by-garden.component.html',
  styleUrls: ['./by-garden.component.css']
})
export class ByGardenComponent implements OnInit, OnDestroy {
  private readonly destroyed$ = new Subject<void>();
  private readonly objectsWithGardenInAmsterdamPulledSuccess$ = new Subject<void>();
  private readonly objectsWithGardenInAmsterdamPulledError$ = new Subject<void>();
  private readonly pullingObjectsWithGardenInAmsterdamOmitted$ = new Subject<void>();

  private _dataUpdating$ = new BehaviorSubject<boolean>(false);
  public get dataUpdating$(): Observable<boolean> {
    return this._dataUpdating$.asObservable();
  }

  private _top10Agents$ = new BehaviorSubject<Top10Agents>(<Top10Agents> {agents: []});
  public get top10Agents$(): Observable<Top10Agents> {
    return this._top10Agents$.asObservable();
  }

  constructor(private readonly hubService: Top10HubService,
              private readonly dataService: Top10DataService,
              private readonly snackBar: MatSnackBar) { }

  ngOnInit() {
    this.getCurrentData();

    this.objectsWithGardenInAmsterdamPulledSuccess$.pipe(takeUntil(this.destroyed$)).subscribe(async () => {
      await this.getCurrentData();
      this.snackBar.open("Table was automatically updated", "Ok", {duration: 10000});
    });

    this.objectsWithGardenInAmsterdamPulledError$.pipe(takeUntil(this.destroyed$)).subscribe(() => {
      this.snackBar.open("Something went wrong when pulling data from Funda", "Ok", {duration: 10000});
    });

    this.pullingObjectsWithGardenInAmsterdamOmitted$.pipe(takeUntil(this.destroyed$)).subscribe(() => {
      this.snackBar.open("Request for updating data was omitted due the high rate of requests Funda. Please try again later.", "Ok", {duration: 10000});
      this._dataUpdating$.next(false);
    });

    this.hubService.addTransferEventListener(HubMethods.ObjectsWithGardenInAmsterdamPulledSuccess, this.objectsWithGardenInAmsterdamPulledSuccess$);
    this.hubService.addTransferEventListener(HubMethods.ObjectsWithGardenInAmsterdamPulledError, this.objectsWithGardenInAmsterdamPulledError$);
    this.hubService.addTransferEventListener(HubMethods.PullingObjectsWithGardenInAmsterdamOmitted, this.pullingObjectsWithGardenInAmsterdamOmitted$);
  }

  async requestUpdate(): Promise<void> {
    this._dataUpdating$.next(true);
    return this.dataService.requestsUpdateAllObjectsWithGardenInAmsterdam();
  }

  async getCurrentData(): Promise<void> {
    this._dataUpdating$.next(true);
    const data = await this.dataService.getTop10AgentsByGarden();
    this._dataUpdating$.next(false);
    this._top10Agents$.next(data);
  }

  ngOnDestroy(): void {
    this.destroyed$.next();
    this.destroyed$.complete();
  }
}
