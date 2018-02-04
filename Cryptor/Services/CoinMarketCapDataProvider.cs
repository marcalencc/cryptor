using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Cryptor.Model;
using Newtonsoft.Json;

namespace Cryptor.Services
{
    public class CoinMarketCapDataProvider : IDataProvider
    {
        private static readonly HttpClient client = new HttpClient();
        public string ApiEndPoint { get; }

        public CoinMarketCapDataProvider()
        {
            ApiEndPoint = @"https://api.coinmarketcap.com/v1/ticker/?limit=0";
        }

        public async Task<List<Currency>> GetData()
        {
            using (var client = new HttpClient())
            {
                using (var r = await client.GetAsync(new Uri(ApiEndPoint)))
                {
                    string result = await r.Content.ReadAsStringAsync();
                    List<Currency> currencies = JsonConvert.DeserializeObject<List<Currency>>(result);
                    return currencies;
                }
            }
        }
    }
}
