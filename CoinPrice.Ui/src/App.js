import React, { useState, useEffect } from 'react';
import {
  ChakraProvider,
  Box,
  theme,
  Select,
  Button,
  FormLabel,
  Input,
  Heading,
} from '@chakra-ui/react';
import { Formik } from 'formik';

function App() {
  // Initilise state
  const [state, setCoinDetails] = useState({
    CoinType: 'BTC',
    Ask: 0.0,
    PercentageChanged: '0%',
  });
  const baseApiUrl =
    process.env.NODE_ENV === 'production'
      ? 'https://coin-price-api.azurewebsites.net'
      : ''; // name resolved to http://coinprice.api:80 from proxy config

  // Handler to refresh price for preferred coin
  const refreshHandler = async () => {
    // Pull price data for preferred coin
    const response = await fetch(`${baseApiUrl}/api/price`).then(resp =>
      resp.json()
    );

    // Calculate percentage changed
    const percentageChanged =
      state.Ask === 0 ? 0 : ((response.ask - state.Ask) / state.Ask) * 100;

    // Update State
    const result = {
      CoinType: response.coinType,
      Ask: response.ask,
      PercentageChanged:
        response.coinType === state.CoinType ? `${percentageChanged}%` : '0%',
    };
    setCoinDetails(result);

    return result;
  };

  // Handler to set user preferred coin
  const setPreferredHandler = async values => {
    const options = {
      method: 'PUT',
    };
    await fetch(`${baseApiUrl}/api/user/${values.CoinType}`, options);
    setCoinDetails({ ...state, ...{ PercentageChanged: '0%' } }); // changing preference reset %
  };

  // Populate form on load
  useEffect(() => {
    refreshHandler();
  }, []);
  return (
    <ChakraProvider theme={theme}>
      <Box maxW="800px" w="100%" mx="auto">
        <center>
          <Heading>Coin Price Application</Heading>
        </center>
        {/* Set preferred coin form */}
        <Formik
          initialValues={{ ...state }}
          onSubmit={(values, { setSubmitting }) => {
            setPreferredHandler(values).then(_ =>
              refreshHandler().then(_ => setSubmitting(false))
            );
          }}
          enableReinitialize
        >
          {({ values, handleChange, handleSubmit, isSubmitting }) => (
            <form onSubmit={handleSubmit}>
              <FormLabel style={{ display: 'block' }}>Select Coin</FormLabel>
              <Select
                name="CoinType"
                value={values.CoinType}
                onChange={handleChange}
                style={{ display: 'block' }}
              >
                <option value="BTC">BTC</option>
                <option value="ETH">ETH</option>
                <option value="XRP">XRP</option>
              </Select>
              <Button type="submit" isLoading={isSubmitting} colorScheme="teal">
                Set preference
              </Button>
            </form>
          )}
        </Formik>
        {/* Refresh details form */} {/*TODO - MOVE TO SEPARATE COMPOENT*/}
        <Formik
          initialValues={{ ...state }}
          onSubmit={(values, { setSubmitting }) => {
            refreshHandler().then(_ => {
              setSubmitting(false);
            });
          }}
          enableReinitialize
        >
          {({ values, handleChange, handleSubmit, isSubmitting }) => (
            <form onSubmit={handleSubmit}>
              <FormLabel>AskValue:</FormLabel>
              <Input readOnly value={values.Ask}></Input>
              <FormLabel>Percentage Changed:</FormLabel>
              <Input readOnly value={values.PercentageChanged}></Input>
              <Button type="submit" isLoading={isSubmitting} colorScheme="teal">
                Refresh
              </Button>
            </form>
          )}
        </Formik>
      </Box>
    </ChakraProvider>
  );
}

export default App;
