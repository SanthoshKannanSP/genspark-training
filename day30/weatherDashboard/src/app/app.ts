import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { CitySearchComponent } from "./city-search-component/city-search-component";
import { WeatherCardComponent } from "./weather-card-component/weather-card-component";

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, CitySearchComponent, WeatherCardComponent],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected title = 'weatherDashboard';
}
