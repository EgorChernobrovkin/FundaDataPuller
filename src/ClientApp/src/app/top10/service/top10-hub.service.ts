import { Injectable } from '@angular/core';
import * as signalR from '@aspnet/signalr';
import { Subject } from "rxjs";
import { HubMethods } from "../models/hub-methods";

@Injectable({
  providedIn: 'root'
})
export class Top10HubService {
  private top10HubConnection: signalR.HubConnection;

  public startConnection(): void {
    this.top10HubConnection =
      new signalR.HubConnectionBuilder()
        .withUrl("https://localhost:5001/hub/top10")
        .build();

    this.top10HubConnection.start()
      .then(() => console.log('Connection with Top10Hub established'))
      .catch(err => console.log('Error while establishing connection with Top10Hub: ' + err));
  }

  public addTransferEventListener(methodName: HubMethods, subject: Subject<void>) {
    this.top10HubConnection.on(methodName.toString(), () => {
      console.log(`Message received: ${methodName}`);
      subject.next();
    });
  }
}
