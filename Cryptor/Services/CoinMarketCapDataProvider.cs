﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Cryptor.Model;
using Newtonsoft.Json;
using Cryptor.CommonResources;

namespace Cryptor.Services
{
    public class CoinMarketCapDataProvider : IDataProvider
    {
        public async Task<List<Currency>> GetData()
        {
            int retryCount = 0;
            do
            {
                using (var client = new HttpClient())
                {
                    try
                    {
                        using (var r = await client.GetAsync(new Uri(@"https://api.coinmarketcap.com/v1/ticker/?limit=0")))
                        {
                            string result = await r.Content.ReadAsStringAsync();
                            List<Currency> currencies = JsonConvert.DeserializeObject<List<Currency>>(result);
                            return currencies;
                        }
                    }
                    catch (HttpRequestException)
                    {
                    }
                }
            }
            while (++retryCount <= Constants.MAX_RETRIES);

            return null;
        }
    }
}
