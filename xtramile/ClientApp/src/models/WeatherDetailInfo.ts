import { Weather } from "./Weather";

export interface WeatherDetailInfo {
    dt: number;
    sunrise: number;
    sunset: number;
    temp: number;
    temp_celcius: number;
    feels_like: number;
    pressure: number;
    humidity: number;
    dew_point: number;
    uvi: number;
    clouds: number;
    visibility: number;
    wind_speed: number;
    wind_deg: number;
    wind_gust: number;
    weather: Weather[];
}