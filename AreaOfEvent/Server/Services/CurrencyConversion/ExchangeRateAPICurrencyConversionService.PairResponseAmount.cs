namespace AreaOfEvent.Server.Services.CurrencyConversion
{
    public partial class ExchangeRateAPICurrencyConversionService
    {
        class PairResponseAmount : PairResponse
        {
            //"conversion_result": 5.8884

            public double conversion_result { get; set; }
        }
    }


}
