import { Autocomplete, TextField, CircularProgress, AutocompleteChangeDetails, AutocompleteChangeReason } from "@mui/material";
import axios from "axios";
import React, { useState, useEffect } from "react";
import { locationAPIUrl } from "../../shared/links";

interface CitySelectorProps {
  countryName?: string;
  onChange: (city?: string) => void;
}

export const CitySelector = (props: CitySelectorProps) => {
  const [isLoading, setIsLoading] = useState(false);
  const [cities, setCities] = useState<string[]>([]);
  const [city, setCity] = useState<string | undefined>(undefined);

  const getCities = async () => {
    if (!props.countryName) {
      return;
    }

    setIsLoading(true);
    setCities([]);
    setCity('');

    try {
      const response = await axios.get<string[]>(
        `${locationAPIUrl}/country/${props.countryName}/cities`
      );

      if (response.status !== 200) {
        throw response.statusText;
      }

      const cities = response.data;
      setCities([...cities]);
    } finally {
      setIsLoading(false);
    }
  };

  const onChange = (event: React.SyntheticEvent<Element, Event>, value: string | null, reason: AutocompleteChangeReason, details?: AutocompleteChangeDetails<string> | undefined) => {
    setCity(value === null ? undefined : value);
  };

  useEffect(
    () => {
      getCities();
    },
    // eslint-disable-next-line
    [props.countryName]
  );

  useEffect(() => props.onChange(city));

  return (
    <Autocomplete
      id="countries"
      options={cities}
      isOptionEqualToValue={(option, value) => option === value}
      getOptionLabel={(option) => option}
      loading={isLoading}
      value={city || ''}
      onChange={onChange}
      renderInput={(params) => (
        <TextField
          {...params}
          label="City"
          InputProps={{
            ...params.InputProps,
            endAdornment: (
              <React.Fragment>
                {isLoading ? (
                  <CircularProgress color="inherit" size={20} />
                ) : null}
                {params.InputProps.endAdornment}
              </React.Fragment>
            ),
          }}
        />
      )}
      disabled={isLoading || !props.countryName}
      fullWidth
    />
  );
};
