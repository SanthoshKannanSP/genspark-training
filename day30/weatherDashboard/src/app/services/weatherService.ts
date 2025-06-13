import { Injectable } from "@angular/core";
import { BehaviorSubject } from "rxjs";

@Injectable()
export class WeatherService
{
    private cityName = new BehaviorSubject<string>("Enter a city name");
    private temperature = new BehaviorSubject<string>("-");
    private humidity = new BehaviorSubject<string>("-");
    private windSpeed = new BehaviorSubject<string>("-");

    cityName$ = this.cityName.asObservable();
    temperature$ = this.temperature.asObservable();
    humidity$ = this.humidity.asObservable(); 
    windspeed$ = this.windSpeed.asObservable();

    apiKey = "";

    async updateWeather(city:string)
    {
        if (city.trim() === "") {
            alert("Enter a city name!");
        } else {
        let locationDetails = await this.getLocationDetails(city.trim());
        if (!locationDetails[0]) {
            alert("Invalid city name!");
            throw new Error("Invalid city name");
        }
        let cityName = locationDetails[0].name;
        let weatherDetails = await this.getWeatherDetails(
            locationDetails[0].lat,
            locationDetails[0].lon
        );
        this.cityName.next(cityName);
        this.temperature.next(weatherDetails.main.temp);
        this.humidity.next(weatherDetails.main.humidity);
        let weatherspeed = (weatherDetails.wind.speed * 3.6).toFixed(2);
        this.windSpeed.next(weatherspeed);
        }
    }

    async getLocationDetails(cityName:string) {
    let endpoint = `https://api.openweathermap.org/geo/1.0/direct?q=${cityName}&limit=1&appid=${this.apiKey}`;
    try {
      const response = await fetch(endpoint);
      if (!response.ok) {
        alert("Error fetching the data");
        throw new Error(`Response status ${response.statusText}`);
      }

      const data = response.json();
      return data;
    } catch (error:any) {
      console.error(error.message);
    }
    }

    async getWeatherDetails(latitude:string, longitude:string) {
        let endpoint = `https://api.openweathermap.org/data/2.5/weather?lat=${latitude}&lon=${longitude}&appid=${this.apiKey}&units=metric`;
        try {
        const response = await fetch(endpoint);
        if (!response.ok) {
            alert("Error fetching the data");
            throw new Error(`Response status ${response.statusText}`);
        }

        const data = response.json();
        return data;
        } catch (error:any) {
        console.error(error.message);
        }
    }
}