namespace AreaOfEvent.Server.Services.AddressLookup.Data
{
    public struct AddressLookupResult
    {
        public enum Status
        {
            OK,
            Error
        }
        public Address Address { get; init; }

        public Status LookupStatus { get; init; }

        public string Error { get; init; }

        public bool IsValid => LookupStatus == Status.OK;


        public static AddressLookupResult ErrorResult( string error ) => new() { LookupStatus = Status.Error, Address = null, Error = error };

        public static AddressLookupResult CreateFrom( Address a ) => new() { LookupStatus = Status.OK, Address = a, Error = string.Empty };
    }

}
