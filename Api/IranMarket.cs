using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyBot.Api
{
    public class IranMarket
    {
        private string RequestAddress { get; } = "https://raw.githubusercontent.com/iran-market/iran-market.github.io/main/data/popular.json";

        public async Task<CurrencyResponse?> GetCurrency()
        {
            HttpClient client = new();
            try
            {
                HttpResponseMessage httpResponse = await client.GetAsync(RequestAddress);

                CurrencyResponse? response = JsonConvert.DeserializeObject<CurrencyResponse>(await httpResponse.Content.ReadAsStringAsync());

                return response;
            }
            catch
            {
                return await GetCurrency();
            }
        }

    }
}
