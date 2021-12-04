import { Autocomplete, TextField, CircularProgress, AutocompleteChangeDetails, AutocompleteChangeReason, Typography, useTheme, useMediaQuery } from "@mui/material";
import axios from "axios";
import React, { useState, useEffect } from "react";
import { locationAPIUrl } from "../../shared/links";
import { ListChildComponentProps, VariableSizeList } from 'react-window';

interface CitySelectorProps {
  countryName?: string;
  onChange: (city?: string) => void;
}

const LISTBOX_PADDING = 8;

const renderRow = (props: ListChildComponentProps) => {
  const { data, index, style } = props;
  const dataSet = data[index];
  const inlineStyle = {
    ...style,
    top: (style.top as number) + LISTBOX_PADDING,
  };

  return (
    <Typography component="li" {...dataSet[0]} noWrap style={inlineStyle}>
      {dataSet[1]}
    </Typography>
  );
};

const OuterElementContext = React.createContext({});

const OuterElementType = React.forwardRef<HTMLDivElement>((props, ref) => {
  const outerProps = React.useContext(OuterElementContext);
  return <div ref={ref} {...props} {...outerProps} />;
});

const useResetCache = (data: any) => {
  const ref = React.useRef<VariableSizeList>(null);
  React.useEffect(() => {
    if (ref.current != null) {
      ref.current.resetAfterIndex(0, true);
    }
  }, [data]);
  return ref;
}

const ListboxComponent = React.forwardRef<
  HTMLDivElement,
  React.HTMLAttributes<HTMLElement>
>(function ListboxComponent(props, ref) {
  const { children, ...other } = props;
  const itemData: React.ReactChild[] = [];
  (children as React.ReactChild[]).forEach(
    (item: React.ReactChild & { children?: React.ReactChild[] }) => {
      itemData.push(item);
      itemData.push(...(item.children || []));
    },
  );

  const theme = useTheme();
  const smUp = useMediaQuery(theme.breakpoints.up('sm'), {
    noSsr: true,
  });
  const itemCount = itemData.length;
  const itemSize = smUp ? 36 : 48;

  const getChildSize = (child: React.ReactChild) => {
    if (child.hasOwnProperty('group')) {
      return 48;
    }

    return itemSize;
  };

  const getHeight = () => {
    if (itemCount > 8) {
      return 8 * itemSize;
    }
    return itemData.map(getChildSize).reduce((a, b) => a + b, 0);
  };

  const gridRef = useResetCache(itemCount);

  return (
    <div ref={ref}>
      <OuterElementContext.Provider value={other}>
        <VariableSizeList
          itemData={itemData}
          height={getHeight() + 2 * LISTBOX_PADDING}
          width="100%"
          ref={gridRef}
          outerElementType={OuterElementType}
          innerElementType="ul"
          itemSize={(index) => getChildSize(itemData[index])}
          overscanCount={5}
          itemCount={itemCount}
        >
          {renderRow}
        </VariableSizeList>
      </OuterElementContext.Provider>
    </div>
  );
});

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
      disableListWrap
      ListboxComponent={ListboxComponent}
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
      renderOption={(props, option) => [props, option]}
      disabled={isLoading || !props.countryName}
      fullWidth
    />
  );
};
