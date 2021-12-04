import {
  CircularProgress,
  Grid,
  Table,
  TableBody,
  TableCell,
  TableRow,
  Typography,
} from "@mui/material";
import axios from "axios";
import { useEffect, useState } from "react";
import { WeatherInfo as Info } from "../../models/WeatherInfo";
import { weatherAPIUrl } from "../../shared/links";
interface WeatherInfoProps {
  cityName?: string;
}

export const WeatherInfo = (props: WeatherInfoProps) => {
  const [isLoading, setIsLoading] = useState(false);
  const [info, setInfo] = useState<Info | undefined>(undefined);

  const getWeatherInfo = async () => {
    setInfo(undefined);

    if (!props.cityName) {
      return;
    }

    setIsLoading(true);

    try {
      const response = await axios.get<Info>(
        `${weatherAPIUrl}/${props.cityName}`
      );

      if (response.status !== 200) {
        throw response.statusText;
      }

      setInfo(response.data);
    } finally {
      setIsLoading(false);
    }
  };

  useEffect(
    () => {
      getWeatherInfo();
    },
    // eslint-disable-next-line
    [props.cityName]
  );

  return (
    <Grid container spacing={1} direction="column">
      {
        isLoading &&
        <Grid item xs={12}>
          <CircularProgress />
        </Grid>
      }
      <Grid item xs={12}>
        <img
          src={`https://openweathermap.org/img/w/${info?.current.weather[0].icon}.png`}
          alt="weather-icon"
        />
        <Typography variant="body1">{props.cityName}</Typography>
        <Typography variant="body1">
          {`(${info?.current.weather[0].main})`}
        </Typography>
        <Typography variant="subtitle1">
          {`${new Date((info?.current.dt || 0) * 1000).toLocaleString()}`}
        </Typography>
      </Grid>
      <Grid item xs={12}>
        <Table>
          <TableBody>
            <TableRow>
              <TableCell rowSpan={2}>Location</TableCell>
              <TableCell>Latitude</TableCell>
              <TableCell>{info?.lat}</TableCell>
            </TableRow>
            <TableRow>
              <TableCell>Longitude</TableCell>
              <TableCell>{info?.lon}</TableCell>
            </TableRow>
            <TableRow>
              <TableCell rowSpan={3}>Wind</TableCell>
              <TableCell>Speed</TableCell>
              <TableCell>{`${info?.current.wind_speed} miles/hour`}</TableCell>
            </TableRow>
            <TableRow>
              <TableCell>Degrees</TableCell>
              <TableCell>{info?.current.wind_deg}</TableCell>
            </TableRow>
            <TableRow>
              <TableCell>Gust</TableCell>
              <TableCell>{`${info?.current.wind_gust} miles/hour`}</TableCell>
            </TableRow>
            <TableRow>
              <TableCell>Visibility</TableCell>
              <TableCell
                colSpan={2}
              >{`${info?.current.visibility.toLocaleString()} metres`}</TableCell>
            </TableRow>
            <TableRow>
              <TableCell>Sky</TableCell>
              <TableCell
                colSpan={2}
              >{`${info?.current.clouds.toLocaleString()} % clouds`}</TableCell>
            </TableRow>
            <TableRow>
              <TableCell rowSpan={2}>Temperature</TableCell>
              <TableCell>Celcius</TableCell>
              <TableCell>{`${info?.current.temp_celcius.toLocaleString()} degrees`}</TableCell>
            </TableRow>
            <TableRow>
              <TableCell>Fahrenheit</TableCell>
              <TableCell>{`${info?.current.temp.toLocaleString()} degrees`}</TableCell>
            </TableRow>
            <TableRow>
              <TableCell>Dew Point</TableCell>
              <TableCell
                colSpan={2}
              >{`${info?.current.dew_point.toLocaleString()} degrees Fahrenheit`}</TableCell>
            </TableRow>
            <TableRow>
              <TableCell>Relative Humidity</TableCell>
              <TableCell
                colSpan={2}
              >{`${info?.current.humidity.toLocaleString()} %`}</TableCell>
            </TableRow>
            <TableRow>
              <TableCell>Pressure</TableCell>
              <TableCell
                colSpan={2}
              >{`${info?.current.pressure.toLocaleString()} hPa`}</TableCell>
            </TableRow>
          </TableBody>
        </Table>
      </Grid>
    </Grid>
  );
};
