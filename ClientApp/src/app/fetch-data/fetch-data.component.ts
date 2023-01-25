import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public forecasts: WeatherForecast[] = [];
  public url : string = "";
  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.url = baseUrl;
    //http.get<WeatherForecast[]>(baseUrl + 'weatherforecast').subscribe(result => {
    //  this.forecasts = result;
    //}, error => console.error(error));


    //segun la pagina oficial se depreco el subscribe con varios args
    //esta seria la mejor forma
    http.get<WeatherForecast[]>('https://localhost:44420/' + 'weatherforecast').subscribe({
      next: (v) => this.forecasts = v,
      error: (e) => console.error(e),
      complete: () => console.info('complete') 
    });
  }
}

interface WeatherForecast {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}
