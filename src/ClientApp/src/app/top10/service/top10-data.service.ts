import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { map } from "rxjs/operators";
import {Top10Agents} from "../models/top10-agents.model"

@Injectable({
  providedIn: 'root'
})
export class Top10DataService {
  private readonly baseUrl = 'https://localhost:5001/api/top10/';

  constructor(private readonly httpClient: HttpClient) {
  }

  requestsUpdateAllObjectsInAmsterdam(): Promise<void> {
    return this.httpClient.put(this.baseUrl + "all", {}).pipe(map(value => {return;})).toPromise();
  }

  requestsUpdateAllObjectsWithGardenInAmsterdam(): Promise<void> {
    return this.httpClient.put(this.baseUrl + "byGarden", {}).pipe(map(value => {return;})).toPromise();
  }

  getTop10Agents(): Promise<Top10Agents> {
    return this.httpClient.get<Top10Agents>(this.baseUrl + "all").toPromise()
  }

  getTop10AgentsByGarden(): Promise<Top10Agents> {
    return this.httpClient.get<Top10Agents>(this.baseUrl + "byGarden").toPromise()
  }
}
