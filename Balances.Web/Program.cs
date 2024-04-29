using Balances.Http.Client;
using Balances.ViewModel;
using Balances.Web;
using Balances.Web.Pages;
using Balances.Web.Services.Contracts;
using Balances.Web.Services.Implementation;
using Blazored.LocalStorage;
using Blazored.SessionStorage;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Http;
using Radzen;

var builder = WebAssemblyHostBuilder.CreateDefault(args);


builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");




//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://balanceapi.justicia.ar/") });

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7172/") });

//https://localhost:7172/

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<DialogService>();

builder.Services.AddScoped<ICaratulaClientService, CaratulaClientService>();
builder.Services.AddScoped<IContadorClientService, ContadorService>();
builder.Services.AddScoped<IAutoridadClientService, AutoridadClientService>();
builder.Services.AddScoped<ISociosClientService, SociosClientService>();
builder.Services.AddScoped<ILibrosClientService, LibrosService>();
builder.Services.AddScoped<IArchivosClientService, ArchivosClientService>();

//builder.Services.AddScoped<IEstadoContableClientService, EstadoContableService>();

builder.Services.AddScoped<EstadoContableService>();
builder.Services.AddScoped<EstadoContableViewModel>();


builder.Services.AddScoped<IPresentacionClientService, PresentacionClientService>();
builder.Services.AddScoped<IPresentacionClientService, PresentacionClientService>();
builder.Services.AddScoped<IBaseSessionClientService, BaseSessionClientService>();
builder.Services.AddScoped<IBusquedaDeSociedadesClientService, BusquedaDesociedadesClientService>();
builder.Services.AddScoped<IBalanceClientService, BalanceClientService>();


// Registra BaseService<T>
builder.Services.AddScoped(typeof(BaseService<>));


// Ejemplo de configuraci�n para ASP.NET Core
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


/*builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 6000000; // Set the limit to a larger value (e.g., 6 MB)
});*/



//builder.Services.AddBlazoredLocalStorageAsSingleton();
builder.Services.AddBlazoredSessionStorage();
builder.Services.AddBlazoredLocalStorage();
await builder.Build().RunAsync();
