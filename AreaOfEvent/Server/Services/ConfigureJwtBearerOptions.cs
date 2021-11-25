using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using AreaOfEvent.Shared.Chatting;

namespace AreaOfEvent.Server.Services
{
    public class ConfigureJwtBearerOptions : IPostConfigureOptions<JwtBearerOptions>
    {
        public void PostConfigure( string name, JwtBearerOptions options ) 
        {
            var originalEvent = options.Events.OnMessageReceived;
            options.Events.OnMessageReceived = async context =>
            {
                await originalEvent( context );
                if (string.IsNullOrEmpty( context.Token ))
                {
                    var token = context.Request.Query["access_token"];
                    var path = context.HttpContext.Request.Path;
                    if (!string.IsNullOrEmpty( token ) && path.StartsWithSegments( IChatServerMethods.EndpointName ))
                    {
                        context.Token = token;
                    }
                }
            };
        }
    }
}
