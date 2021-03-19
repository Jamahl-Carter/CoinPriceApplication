using System;
using System.Net.Http;
using System.Threading.Tasks;
using CoinPrice.Contract;
using CoinPrice.Data.Dto;
using Newtonsoft.Json;

namespace CoinPrice.Data.Client.Implementation
{
    internal class CointreeClient : ICointreeClient
    {
        private readonly HttpClient _httpClient;

        public CointreeClient(HttpClient httpClient) => _httpClient = httpClient;

        public async Task<PriceResponse> GetPriceAsync(CoinType coinType)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"prices/aud/{coinType}");

            if (response.IsSuccessStatusCode == false)
                throw new Exception($"Error connecting to cointree API with StatusCode: {response.StatusCode}.");

            string rawContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            return JsonConvert.DeserializeObject<PriceResponse>(rawContent);
        }
    }
}
