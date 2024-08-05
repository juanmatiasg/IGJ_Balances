using Balances.Utilities;
using Balances.Web;
using Balances.Web.Services.Contracts;
using Balances.Web.Services.Implementation;
using Blazored.LocalStorage;
using Blazored.SessionStorage;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Http;
using MudBlazor.Services;
using Radzen;

var builder = WebAssemblyHostBuilder.CreateDefault(args);


builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


// S W E E T  A L E RT
builder.Services.AddSweetAlert2();

// R A D Z E N  N O T I F I C A C I O N E S
builder.Services.AddScoped<NotificationService>();

//builder.Services.AddScoped(sp => new HttpClient
//{
//    BaseAddress = new Uri("https://balanceapi.justicia.ar/"),
//    Timeout = TimeSpan.FromSeconds(150)
//});

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7172/") });

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

//https://localhost:7172/

builder.Services.AddHttpContextAccessor();


builder.Services.AddScoped<ICaratulaClientService, CaratulaClientService>();
builder.Services.AddScoped<IContadorClientService, ContadorService>();
builder.Services.AddScoped<IAutoridadClientService, AutoridadClientService>();
builder.Services.AddScoped<ISociosClientService, SociosClientService>();
builder.Services.AddScoped<ILibrosClientService, LibrosService>();
builder.Services.AddScoped<IArchivosClientService, ArchivosClientService>();
builder.Services.AddScoped<IEstadoContableClientService, EstadoContableService>();
builder.Services.AddScoped<IPresentacionClientService, PresentacionClientService>();
builder.Services.AddScoped<IPresentacionClientService, PresentacionClientService>();
builder.Services.AddScoped<ISessionClientService, SessionClientService>();
builder.Services.AddScoped<IBusquedaDeSociedadesClientService, BusquedaDesociedadesClientService>();
builder.Services.AddScoped<IBalanceClientService, BalanceClientService>();

builder.Services.AddScoped<DialogService>();
builder.Services.AddRadzenComponents();
builder.Services.AddMudServices();

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


builder.Services.AddBlazoredSessionStorage();
builder.Services.AddBlazoredLocalStorage();
await builder.Build().RunAsync();
