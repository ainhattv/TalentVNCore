import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { SignalRService } from '../services/signal-r.service';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent implements OnInit {
  public forecasts: WeatherForecast[];
  public _http: HttpClient;

  public chartOptions: any = {
    scaleShowVerticalLines: true,
    responsive: true,
    scales: {
      yAxes: [{
        ticks: {
          beginAtZero: true
        }
      }]
    }
  };
  public chartLabels: string[] = ['Real time data for the chart'];
  public chartType: string = 'bar';
  public chartLegend: boolean = true;
  public colors: any[] = [{ backgroundColor: '#5491DA' }, { backgroundColor: '#E74C3C' }, { backgroundColor: '#82E0AA' }, { backgroundColor: '#E5E7E9' }]

  constructor(public signalRService: SignalRService, http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<WeatherForecast[]>(baseUrl + 'api/SampleData/WeatherForecasts').subscribe(result => {
      this.forecasts = result;
    }, error => console.error(error));

    this._http = http;
  }

  ngOnInit() {
    this.signalRService.startConnection();
    this.signalRService.addTransferChartDataListener();

    this.signalRService.addBroadcastChartDataListener();

    this.startHttpRequest();
  }

  private startHttpRequest = () => {
    this._http.get('https://localhost:5001/api/chart')
      .subscribe(res => {
        console.log(res);
      })
  }

  public chartClicked = (event) => {
    console.log(event);
    this.signalRService.broadcastChartData();
  }

}

interface WeatherForecast {
  dateFormatted: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}
