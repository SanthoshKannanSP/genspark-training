import { Component, inject } from '@angular/core';
import { WeatherService } from '../services/weatherService';

@Component({
  selector: 'app-weather-card-component',
  imports: [],
  templateUrl: './weather-card-component.html',
  styleUrl: './weather-card-component.css'
})
export class WeatherCardComponent {
    weatherService = inject(WeatherService);
    cityName:string = "Search a city name";
    temperature:string = "-";
    humidity:string = "-";
    windspeed:string = "-";
    
    constructor()
    {
      this.weatherService.cityName$.subscribe({
        next: data => this.cityName = data
      });
      
      this.weatherService.temperature$.subscribe({
        next: data => this.temperature = data
      });
      this.weatherService.humidity$.subscribe({
        next: data => this.humidity = data
      });
      this.weatherService.windspeed$.subscribe({
        next: data => this.windspeed = data
      });
    }
}
