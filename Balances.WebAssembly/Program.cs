using Balances.Services.Contract;
using Balances.Services.Implementation;
using Balances.WebAssembly;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Http;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");



builder.Services.AddBlazoredSessionStorage();
builder.Services.AddScoped<HttpClient>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:7172/") });



await builder.Build().RunAsync();
