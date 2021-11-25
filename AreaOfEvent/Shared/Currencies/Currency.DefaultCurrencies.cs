namespace AreaOfEvent.Shared.Currencies
{
    public partial class Currency
    {
        static class DefaultCurrencies
        {
            public static readonly Currency[] Data = new Currency[]
            {
                new Currency {
                    ID = -1,
                    Code ="EUR",
                    CurrencyName ="Euro",
                    Symbol ="€"
                } ,
                new Currency{
                    ID = -2,
                    Code ="USD",
                    CurrencyName ="United States dollar",
                    Symbol ="$"
                }
            };
        }

    }
}
