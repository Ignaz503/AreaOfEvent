namespace AreaOfEvent.Server.Services.CurrencyConversion
{
    public partial class ExchangeRateAPICurrencyConversionService
    {
        class PairResponse
        {
            //"result": "success",
            //"documentation": "https://www.exchangerate-api.com/docs",
            //"terms_of_use": "https://www.exchangerate-api.com/terms",
            //"time_last_update_unix": 1585267200,
            //"time_last_update_utc": "Fri, 27 Mar 2020 00:00:00 +0000",
            //"time_next_update_unix": 1585270800,
            //"time_next_update_utc": "Sat, 28 Mar 2020 01:00:00 +0000",
            //"base_code": "EUR",
            //"target_code": "GBP",
            //"conversion_rate": 0.8412

            public string result { get; set; }
            public string documentation { get; set; }

            public string terms_of_use { get; set; }

            public int time_last_update_unix { get; set; }

            public string time_last_update_utc { get; set; }

            public int time_next_update_unix { get; set; }

            public string time_next_update_utc { get; set; }

            public string base_code { get; set; }

            public string target_code { get; set; }

            public double conversion_rate { get; set; }

        }

    }


}
