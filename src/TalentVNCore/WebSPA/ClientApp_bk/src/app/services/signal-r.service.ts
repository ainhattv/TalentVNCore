import { Injectable, Inject } from '@angular/core';

import * as signalR from "@aspnet/signalr";
import { ChartModel } from '../ChartModel';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {

  _baseUrl: any;

  constructor(@Inject('BASE_URL') baseUrl: string) {
    this._baseUrl = baseUrl;
  }

  public data: ChartModel[];
  public bradcastedData: ChartModel[];

  private hubConnection: signalR.HubConnection

  // Start to connect Server Hub
  public startConnection = () => {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(this._baseUrl + 'chart')
      .build();

    this.hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch(err => console.log('Error while starting connection: ' + err))
  }

  public addTransferChartDataListener = () => {
    this.hubConnection.on('transferchartdata', (data) => {
      this.data = data;
      console.log(data);
    });
  }

  public broadcastChartData = () => {
    this.hubConnection.invoke('broadcastchartdata', this.data)
      .catch(err => console.error(err));
  }

  public addBroadcastChartDataListener = () => {
    this.hubConnection.on('broadcastchartdata', (data) => {
      this.bradcastedData = data;
    })
  }

}
