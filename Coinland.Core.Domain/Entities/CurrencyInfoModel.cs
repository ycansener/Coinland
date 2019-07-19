using Newtonsoft.Json;

namespace Coinland.Core.Domain.Entities
{
    [JsonObject("object")]
    public class CurrencyInfoModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("symbol")]
        public string Symbol { get; set; }
        [JsonProperty("rank")]
        public string Rank { get; set; }
        [JsonProperty("price_usd")]
        public decimal USD { get; set; }
        [JsonProperty("price_btc")]
        public decimal BTC { get; set; }
        [JsonProperty("24h_volume_usd")]
        public decimal Volume_24h { get; set; }
        [JsonProperty("market_cap_usd")]
        public decimal MarketCap { get; set; }
        [JsonProperty("available_supply")]
        public decimal AvailableSupply { get; set; }
        [JsonProperty("total_supply")]
        public decimal TotalSupply { get; set; }
        [JsonProperty("percent_change_1h")]
        public decimal? PercentChange_1h { get; set; }
        [JsonProperty("percent_change_24h")]
        public decimal? PercentChange_24h { get; set; }
        [JsonProperty("percent_change_7d")]
        public decimal? PercentChange_7days { get; set; }
        [JsonProperty("last_updated")]
        public string LastUpdated { get; set; }
    }
}
