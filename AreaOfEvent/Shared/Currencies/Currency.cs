using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AreaOfEvent.Shared.Currencies
{
    public partial class Currency
    {
        public int ID { get; init; }

        public string Code { get; init; }

        public string CurrencyName { get; init; }

        public string Symbol { get; init; }

        //public class ModelConfigure : ModelConfigure<Currency>
        //{
        //    public override void ModelSetup( EntityTypeBuilder<Currency> builder )
        //    {
        //        builder.HasKey( nameof( ID ) );

        //        base.ModelSetup( builder );
        //    }

        //    protected override IEnumerable<Currency> SeedData()
        //    {
        //        return DefaultCurrencies.Data;
        //    }

        //}
    }
}
