using AreaOfEvent.Server.Data;
using AreaOfEvent.Server.Hubs;
using AreaOfEvent.Server.Models;
using AreaOfEvent.Server.Services.CurrencyConversion;
using AreaOfEvent.Shared.Chatting;
using Blazored.Modal;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Linq;
using AreaOfEvent.Server.Services;
using IdentityServer4;
using Microsoft.AspNetCore.Identity;

namespace AreaOfEvent.Server
{
    public class Startup
    {
        public Startup( IConfiguration configuration )
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices( IServiceCollection services )
        {
            services.AddSingleton<ICurrencyConversionService, ExchangeRateAPICurrencyConversionService>();

            services.AddDbContext<ApplicationDbContext>( options => {
                options.UseSqlServer(
                    Configuration.GetConnectionString( "DefaultConnection" ) );
                options.LogTo( s => System.Diagnostics.Debug.WriteLine( s ));
            });

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<ApplicationUser>( options => options.SignIn.RequireConfirmedAccount = false )
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddIdentityServer()
                .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

            services.AddBlazoredModal(); //TODO necessary?

            services.AddSignalR();
            services.AddResponseCompression( opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { "application/octet-stream" }
                    );
            } );

            services.AddAuthentication()
                .AddIdentityServerJwt()
                .AddGoogle( googleOptions => {
                    googleOptions.SignInScheme = IdentityConstants.ExternalScheme;
                    googleOptions.ClientId = Configuration["Authentication:Google:ClientId"];
                    googleOptions.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
                } );




            services.TryAddEnumerable(ServiceDescriptor.Singleton<IPostConfigureOptions<JwtBearerOptions>,ConfigureJwtBearerOptions>() );

            services.AddControllersWithViews();
            services.AddControllers();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure( IApplicationBuilder app, IWebHostEnvironment env )
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
                app.UseWebAssemblyDebugging();
            } else
            {
                app.UseExceptionHandler( "/Error" );
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseIdentityServer();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints( endpoints =>
             {
                 endpoints.MapRazorPages();
                 endpoints.MapControllers();
                 endpoints.MapHub<ChatHub>( IChatServerMethods.EndpointName );
                 endpoints.MapFallbackToFile( "index.html" );
             } );
        }
    }
}
