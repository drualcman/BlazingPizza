using BlazingPizza.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BlazingPizza.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            //set the base url to use in the HttpClient calls, Ex. https://apu.mydomain.com
            //default use the same url from wehere we are running if the App is hosted in the same web/api
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            //force to use the culture from the country about the app.
            //CultureInfo.CurrentCulture = new CultureInfo("en-AU");
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-AU");
            CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en-AU");

            //Add AppState Service
            builder.Services.AddScoped<OrderState>();
            builder.Services.AddApiAuthorization();

            await builder.Build().RunAsync();
        }
    }
}
