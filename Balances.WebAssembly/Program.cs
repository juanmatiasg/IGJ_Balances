using Balances.WebAssembly;
using Balances.WebAssembly.Services.Contract;
using Balances.WebAssembly.Services.Implementation;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");




builder.Services.AddScoped<HttpClient>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:7172/") });
builder.Services.AddScoped<IBusquedaCuilOrCorrelativoService, BusquedaCuilOrCorrelativoService>();


await builder.Build().RunAsync();
