import { WeatherDetailInfo } from "./WeatherDetailInfo";

export interface WeatherInfo {
  lat: number;
  lon: number;
  timezone: string;
  timezone_offset: number;
  current: WeatherDetailInfo;
}