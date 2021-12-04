import { CssBaseline } from "@mui/material";
import { StyledEngineProvider } from "@mui/styled-engine";
import { createTheme, ThemeProvider } from "@mui/material/styles";
import { Weather } from "./pages";

const App = () => {
  const theme = createTheme(
    {
      palette: {
        mode: 'light'
      }
    }
  );
  return (
    <StyledEngineProvider injectFirst>
      <ThemeProvider theme={theme}>
        <CssBaseline />
        <Weather />
      </ThemeProvider>
    </StyledEngineProvider>
  );
}

export default App;
