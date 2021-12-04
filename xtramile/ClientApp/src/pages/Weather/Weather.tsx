import { Container, Grid, useTheme } from "@mui/material";
import { useState } from "react";
import { CitySelector, CountrySelector, WeatherInfo } from "../../components";
import { Country } from "../../models/Country";

export const Weather = () => {
  const theme = useTheme();
  const [selectedCountry, setSelectedCountry] = useState<Country | undefined>(
    undefined
  );
  const [selectedCity, setSelectedCity] = useState<string | undefined>(
    undefined
  );

  return (
    <Container
      component="main"
      maxWidth="xl"
      style={{ marginTop: theme.spacing(2) }}
    >
      <Grid container spacing={2} direction="row">
        <Grid item xs={12} md={6} xl={6}>
          <CountrySelector
            onChange={(country) => setSelectedCountry(country)}
          />
        </Grid>
        <Grid item xs={12} md={6} xl={6}>
          <CitySelector
            countryName={selectedCountry?.name}
            onChange={(city) => setSelectedCity(city)}
          />
        </Grid>
        <Grid item xs={12} md={12} xl={12}>
          <WeatherInfo cityName={selectedCity} />
        </Grid>
      </Grid>
    </Container>
  );
};
