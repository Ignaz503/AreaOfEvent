using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AreaOfEvent.Shared.Languages
{
    public partial class Language
    {
        public int ID { get; init; }
        public string Name { get; init; }
        public string ISO639_2_B_Identifier { get; init; }

        //public class ModelConfigure : ModelConfigure<Language>
        //{
        //    public override void ModelSetup( EntityTypeBuilder<Language> builder )
        //    {
        //        builder.HasKey( nameof( ID ) );

        //        base.ModelSetup( builder );
        //    }

        //    protected override IEnumerable<Language> SeedData()
        //    {
        //        return DefaultLanguages.Data;
        //    }
        //}
    }
}
