using AreaOfEvent.Shared.Currencies;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AreaOfEvent.Server.Services.CurrencyConversion
{
    public interface ICurrencyConversionService
    {
        const string ConfigName = "CurrencyConversionAPIKey";

        Task<decimal?> GetConversionRate( Currency from, Currency to );

        Task<decimal?> Convert( decimal amount, Currency from, Currency to );

    }


}
