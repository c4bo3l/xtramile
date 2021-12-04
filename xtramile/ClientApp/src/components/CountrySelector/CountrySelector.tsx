import { Autocomplete, AutocompleteChangeDetails, AutocompleteChangeReason, CircularProgress, TextField } from "@mui/material";
import axios from "axios";
import React from "react";
import { useEffect, useState } from "react";
import { Country } from "../../models/Country";
import { locationAPIUrl } from "../../shared/links";

interface CountrySelectorProps {
  onChange: (country?: Country) => void;
}

export const CountrySelector = (props: CountrySelectorProps) => {
  const [isLoading, setIsLoading] = useState(false);
  const [countries, setCountries] = useState<Country[]>([]);

  const getCountries = async () => {
    setIsLoading(true);

    try {
      const response = await axios.get<Country[]>(
        `${locationAPIUrl}/countries`
      );

      if (response.status !== 200) {
        throw response.statusText;
      }

      const countries = response.data;
      setCountries([...countries]);
    } catch {
      setCountries([]);
    } finally {
      setIsLoading(false);
    }
  };

  const onChange = (event: React.SyntheticEvent<Element, Event>, value: Country | null, reason: AutocompleteChangeReason, details?: AutocompleteChangeDetails<Country> | undefined) => {
    props.onChange(value === null ? undefined : value);
  }

  useEffect(() => {
    getCountries();
  }, []);

  return (
    <Autocomplete
      id="countries"
      options={countries}
      isOptionEqualToValue={(option, value) => option.name === value.name}
      getOptionLabel={(option) => option.name}
      onChange={onChange}
      loading={isLoading}
      renderInput={(params) => (
        <TextField
          {...params}
          label="Country"
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
      disabled={isLoading}
      fullWidth
    />
  );
};
