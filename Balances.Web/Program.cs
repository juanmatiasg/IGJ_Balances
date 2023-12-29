using Balances.Utilities;
using Balances.Web;
using Balances.Web.Services.Contracts;
using Balances.Web.Services.Implementation;
using Blazored.LocalStorage;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Http.Features;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7172/") });

builder.Services.AddScoped<ICaratulaService, CaratulaService>();
builder.Services.AddScoped<IContadorService, ContadorService>();
builder.Services.AddScoped<IAutoridadService, AutoridadService>();
builder.Services.AddScoped<ISociosService, SociosService>();
builder.Services.AddScoped<ILibrosService, LibrosService>();
builder.Services.AddScoped<IArchivosService, ArchivosService>();
builder.Services.AddScoped<IEstadoContableService, EstadoContableService>();

/*builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 2 * 1024 * 1024; // 2MB
});*/


builder.Services.AddBlazoredSessionStorage();
builder.Services.AddBlazoredLocalStorage();


await builder.Build().RunAsync();
