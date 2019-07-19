using Coinland.Core.Domain.Constants;
using Coinland.Core.Domain.Entities;
using Coinland.Core.Domain.Interfaces.Services;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Coinland.Core.Infrastructure.Services
{
    public class CurrencyInfoService : ICurrencyInfoService
    {
        public List<CurrencyInfoModel> GetCurrencyInfo(int? start, int? limit, string convert)
        {
            List<CurrencyInfoModel> currencyInfoList = new List<CurrencyInfoModel>();

            string content = string.Empty;
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // TODO: Url system properties'e çıkarılacak.

                string url = APIUrl.CoinMarketCap + "?";

                if (limit.HasValue)
                {
                    url += "limit=" + limit.Value;
                }
                else
                {
                    url += "limit=0";
                }

                if (start.HasValue)
                {
                    url += "&start=" + start.Value;
                }

                if (!string.IsNullOrEmpty(convert))
                {
                    url += "&convert=" + convert;
                }

                HttpResponseMessage res = client.GetAsync(url).Result;
                content = res.Content.ReadAsStringAsync().Result;
            }

            if (!string.IsNullOrEmpty(content))
            {
                currencyInfoList = JsonConvert.DeserializeObject<List<CurrencyInfoModel>>(content);
            }

            return currencyInfoList;
        }
    }
}
