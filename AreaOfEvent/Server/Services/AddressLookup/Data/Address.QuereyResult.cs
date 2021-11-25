using System;

namespace AreaOfEvent.Server.Services.AddressLookup.Data
{
    public partial class Address
    {
        public struct QuereyResult
        {
            public bool HasValue { get; init; }

            public string Value { get; init; }

            public static readonly QuereyResult NoValue = new(){ HasValue = false, Value = String.Empty};

        }
    }

}
