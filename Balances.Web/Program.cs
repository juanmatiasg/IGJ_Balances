using Balances.Web;
using Balances.Web.Services.Contracts;
using Balances.Web.Services.Implementation;
using Blazored.LocalStorage;
using Blazored.SessionStorage;
using Blazorise;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

var builder = WebAssemblyHostBuilder.CreateDefault(args);


builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");



builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7172/") });
builder.Services.AddHttpContextAccessor();


builder.Services.AddScoped <ICaratulaService, CaratulaService>();
builder.Services.AddScoped<IContadorService, ContadorService>();
builder.Services.AddScoped<IAutoridadService, AutoridadService>();
builder.Services.AddScoped<ISociosService, SociosService>();
builder.Services.AddScoped<ILibrosService, LibrosService>();
builder.Services.AddScoped<IArchivosService, ArchivosService>();
builder.Services.AddScoped<IEstadoContableService, EstadoContableService>();
builder.Services.AddScoped<IPresentacionService, PresentacionService >();
builder.Services.AddScoped<IPresentacionService, PresentacionService>();



// Ejemplo de configuración para ASP.NET Core
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});




builder.Services
    .AddBlazorise(options =>
    {
        options.Immediate = true;
    })
    .AddBootstrapProviders()
    .AddFontAwesomeIcons();






builder.Services.AddBlazoredLocalStorageAsSingleton();
builder.Services.AddBlazoredSessionStorage();
builder.Services.AddBlazoredLocalStorage();

await builder.Build().RunAsync();
