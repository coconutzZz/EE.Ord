using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using EE.Ord.Main.App.Client.HttpRepository;
using EE.Ord.Main.App.Client.Shared;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace EE.Ord.Main.App.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddTransient(
                sp => new HttpClient {BaseAddress = new Uri(builder.HostEnvironment.BaseAddress + "api/")});

            builder.Services.AddScoped<IPatientHttpRepository, PatientHttpRepository>();

            /*builder.Services.AddHttpClient("EE.Ord.Main.App.Server",
                    client =>
                    {
                        client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress + "api/");
                        //client.DefaultRequestHeaders.Add("Accept", "application/json");
                    })
                .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>()
                .CreateClient("EE.Ord.Main.App.Server"));*/

            builder.Services.AddOidcAuthentication(oidcOptions =>
            {
                builder.Configuration.Bind("Local", oidcOptions.ProviderOptions);
                builder.Services.AddCors(corsOptions =>
                {
                    corsOptions.AddPolicy("AllowSpecificOrigins",
                    policy =>
                    {
                        policy.WithOrigins(oidcOptions.ProviderOptions.Authority);
                    });
                });
            });

            builder.Services.AddDevExpressBlazor();
            
            await builder.Build().RunAsync();
        }
    }
}
