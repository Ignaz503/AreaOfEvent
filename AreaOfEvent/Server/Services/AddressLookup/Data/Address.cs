using AreaOfEvent.Shared.Collections;
using AreaOfEvent.Shared.Collections.Extensions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AreaOfEvent.Server.Services.AddressLookup.Data
{
    public partial class Address
    {

        [NotMapped]
        Dictionary<string, string> addressDetails;
        public string DisplayName { get; init; }
        public IEnumerable<EFKeyValuePair<string, string>> AddressDetails
        {
            get
            {
                if (addressDetails == null)
                {
                    addressDetails = new();
                }
                return addressDetails.ConvertToEF();
            }
            init
            {
                addressDetails = new( value.ConvertFromEF() );
            }
        }

        public Address()
        { }

        public Address( Dictionary<string, string> values )
        {
            this.addressDetails = values;
        }


        public QuereyResult Query( string key )
        {
            if (!addressDetails.ContainsKey( key ))
                return QuereyResult.NoValue;

            return new() { HasValue = true, Value = addressDetails[key] };
        }
    }

}
