using AreaOfEvent.Shared.Currencies;
using Microsoft.Extensions.Configuration;
using System;
using System.Globalization;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace AreaOfEvent.Server.Services.CurrencyConversion
{
    public partial class ExchangeRateAPICurrencyConversionService : ICurrencyConversionService
    {
        readonly IConfiguration configuration;

        const string baseURL = "https://v6.exchangerate-api.com/v6/";

        string PairURL { get; init; }

        public ExchangeRateAPICurrencyConversionService( IConfiguration config )
        {
            this.configuration = config;

            this.PairURL = $"{baseURL}{configuration[ICurrencyConversionService.ConfigName]}/pair/";
        }

        public async Task<decimal?> GetConversionRate( Currency from, Currency to )
        {
            var url = $"{PairURL}{from.Code}/{to.Code}";
            var res = await HandleApiCommunication(url, (str)=>
            {
                var obj = JsonSerializer.Deserialize<PairResponse>(str);
                return (decimal)obj.conversion_rate;
            } );
            return res;
        }

        async Task<decimal?> HandleApiCommunication( string url, Func<string, decimal> jsonHandler )
        {
            try
            {
                using (var webClient = new WebClient())
                {
                    var res = await webClient.DownloadStringTaskAsync(url);
                    return jsonHandler( res );
                }
            } catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine( e );
                return null;
            }
        }

        public async Task<decimal?> Convert( decimal amount, Currency from, Currency to )
        {
            var url = $"{PairURL}{from.Code}/{to.Code}/{amount.ToString("F4",CultureInfo.InvariantCulture)}";
            var res = await HandleApiCommunication(url,(str)=>
            {
                var obj = JsonSerializer.Deserialize<PairResponseAmount>(str);
                return (decimal)obj.conversion_result;
            } );
            return res;
        }
    }


}
