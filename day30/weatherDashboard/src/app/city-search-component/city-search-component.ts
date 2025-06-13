import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { WeatherService } from '../services/weatherService';

@Component({
  selector: 'app-city-search-component',
  imports: [FormsModule],
  templateUrl: './city-search-component.html',
  styleUrl: './city-search-component.css'
})
export class CitySearchComponent {
  searchCity:string = "";
  weatherService = inject(WeatherService);

  async handleClick()
  {
    await this.weatherService.updateWeather(this.searchCity);
    this.searchCity = "";
  }

  
}
