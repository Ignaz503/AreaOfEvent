using AreaOfEvent.Server.Data;
using AreaOfEvent.Server.Services.AddressLookup.Data;
using AreaOfEvent.Shared.GeoLocation;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace AreaOfEvent.Server.Services.AddressLookup
{
    public class NominatimAPIAdderessLookupService : IAddressLookupService, IDisposable
    {
        struct QueueData
        {
            public TaskCompletionSource<AddressLookupResult> TSC { get; init; }
            public Location GeoLocation { get; init; }
        }

        //https://nominatim.openstreetmap.org/reverse?format=jsonv2&lat={loc.Lat}&lon={loc.Lng}


        private IConfiguration config;

        string userAgent;

        readonly int rateLimitMilliseconds = 1000;

        private ApplicationDbContext dbContext;

        ConcurrentQueue<QueueData> queue = new();

        CancellationTokenSource tks;

        public NominatimAPIAdderessLookupService( IConfiguration config, ApplicationDbContext dbContext )
        {
            this.config = config;
            this.dbContext = dbContext;
            userAgent = this.config[IAddressLookupService.AddressLookupUserAgent];
            tks = new();
            HandleQueue( tks.Token );
        }

        public Task<AddressLookupResult> LookupAddress( Location geoLocation )
        {
            var tsc = new TaskCompletionSource<AddressLookupResult>();

            queue.Enqueue( new() { TSC = tsc, GeoLocation = geoLocation } );
            return tsc.Task;
        }

        void HandleQueue( CancellationToken tkn )
        {
            Task.Run( async () =>
            {
                while (!tkn.IsCancellationRequested)
                {
                    if (queue.TryDequeue( out QueueData elem ))
                    {
                        var res = await MakeApiCall(elem.GeoLocation);

                        var lookupRes = BuildLookupResult(res);
                        elem.TSC.SetResult( lookupRes );
                    }
                    await Task.Delay( rateLimitMilliseconds, tkn );
                }
            }, tkn );
        }

        private AddressLookupResult BuildLookupResult( JsonElement res )
        {
            if (res.TryGetProperty( "error", out JsonElement error ))
            {
                return AddressLookupResult.ErrorResult( error.GetRawText() );
            }


            var dict = JsonSerializer.Deserialize<Dictionary<string,string>>(
                res.GetProperty("address").GetRawText()
                );
            var displayName = JsonSerializer.Deserialize<string>(res.GetProperty("display_name").GetRawText());
            return AddressLookupResult.CreateFrom( new( dict ) { DisplayName = displayName } );
        }

        async Task<JsonElement> MakeApiCall( Location location )
        {
            using var client =new WebClient();
            client.Headers.Add( "User-Agent", userAgent );


            var url =  $"https://nominatim.openstreetmap.org/reverse?format=jsonv2&lat={location.Latitude.ToString( CultureInfo.InvariantCulture )}&lon={location.Longitude.ToString( CultureInfo.InvariantCulture )}";

            var resStream = await client.OpenReadTaskAsync(url);

            return await JsonSerializer.DeserializeAsync<JsonElement>( resStream );
        }

        public void Dispose()
        {
            tks.Cancel();
        }
    }

}
