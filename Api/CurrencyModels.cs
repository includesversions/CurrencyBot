using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace CurrencyBot.Api
{
    public class CurrencyResponse
    {
        [JsonProperty("data")]
        public List<Currency> Data { get; set; } = new();

        [JsonProperty("meta")]
        public CurrencyMeta Meta { get; set; } = new();
    }

    public class Currency
    {
        [JsonProperty("symbol")]
        public string Symbol { get; set; } = "";

        [JsonProperty("name_fa")]
        public string NameFa { get; set; } = "";

        [JsonProperty("name_en")]
        public string NameEn { get; set; } = "";

        [JsonProperty("category")]
        public string Category { get; set; } = "";

        [JsonProperty("market")]
        public string Market { get; set; } = "";

        [JsonProperty("currency")]
        public string CurrencyCode { get; set; } = "";

        [JsonProperty("unit")]
        public string Unit { get; set; } = "";

        [JsonProperty("unit_size")]
        public int UnitSize { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("bid")]
        public decimal? Bid { get; set; }

        [JsonProperty("ask")]
        public decimal? Ask { get; set; }

        [JsonProperty("open")]
        public decimal? Open { get; set; }

        [JsonProperty("high")]
        public decimal? High { get; set; }

        [JsonProperty("low")]
        public decimal? Low { get; set; }

        [JsonProperty("prev_close")]
        public decimal? PrevClose { get; set; }

        [JsonProperty("change")]
        public decimal? Change { get; set; }

        [JsonProperty("change_pct")]
        public decimal? ChangePct { get; set; }

        [JsonProperty("volume")]
        public decimal? Volume { get; set; }

        [JsonProperty("market_status")]
        public string? MarketStatus { get; set; }

        [JsonProperty("timestamp")]
        public DateTimeOffset? Timestamp { get; set; }

        [JsonProperty("timestamp_tehran")]
        public DateTimeOffset? TimestampTehran { get; set; }

        [JsonProperty("timestamp_jalali")]
        public string? TimestampJalali { get; set; }

        [JsonProperty("checked_at")]
        public DateTimeOffset? CheckedAt { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; } = "";

        [JsonProperty("stale")]
        public bool Stale { get; set; }

        [JsonProperty("quality")]
        public List<string> Quality { get; set; } = new();
    }

    public class CurrencyMeta
    {
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("unit")]
        public string Unit { get; set; } = "";

        [JsonProperty("updated_at")]
        public DateTimeOffset? UpdatedAt { get; set; }

        [JsonProperty("stale")]
        public bool Stale { get; set; }

        [JsonProperty("sources")]
        public List<string> Sources { get; set; } = new();

        [JsonProperty("published_at")]
        public DateTimeOffset? PublishedAt { get; set; }

        [JsonProperty("update_frequency_minutes")]
        public int UpdateFrequencyMinutes { get; set; }

        [JsonProperty("public_file")]
        public string? PublicFile { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; } = "";
    }

}
