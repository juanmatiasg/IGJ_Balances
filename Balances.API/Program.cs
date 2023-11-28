using Balances.Bussiness;
using Balances.Bussiness.Contrato;
using Balances.Bussiness.Implementacion;
using Balances.Repository.Contract;
using Balances.Repository.Implementation;
using Balances.Services.Contract;
using Balances.Services.Implementation;
using Balances.Utilities;
using Dominio.Helpers;
using EmailSender;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Serilog Logs
//Log.Logger = new LoggerConfiguration()
//    .ReadFrom.Configuration(builder.Configuration).CreateLogger();

//Log.Logger = new LoggerConfiguration()
//            .MinimumLevel.Information()
//            .WriteTo.Console()
//            .WriteTo.File("Logs/Log-.txt", rollingInterval: RollingInterval.Day)
//            .CreateLogger();


/// builder.Host.UseSerilog;

//SMTP Settings
builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));

//Balance

builder.Services.AddScoped<IPresentacionBusiness, PresentacionBusiness>();
builder.Services.AddScoped<ICaratulaBusiness, CaratulaBusiness>();
builder.Services.AddScoped<IBalanceBusiness, BalanceBusiness>();
builder.Services.AddScoped<IContadorBusiness, ContadorBusiness>();
builder.Services.AddScoped<IAutoridadesBusiness, AutoridadesBusiness>();
builder.Services.AddScoped<IEstadoContableBusiness, EstadoContableBusiness>();
builder.Services.AddScoped<IArchivoBusiness, ArchivoBusiness>();
builder.Services.AddScoped<ILibrosBusiness, LibrosBusiness>();
builder.Services.AddScoped<ISociosBusiness, SociosBusiness>();

builder.Services.AddSingleton<ISessionService, SessionService>();

builder.Services.AddSingleton<IBalanceService, BalanceService>();
//builder.Services.AddScoped<IContadorService, ContadorService>();
builder.Services.AddScoped<IEstadoContableService, EstadoContableService>();
//builder.Services.AddScoped<IRepresentanteLegalService, RepresentanteLegalService>();
builder.Services.AddScoped<IArchivoService, ArchivoService>();
builder.Services.AddScoped<IPresentacionBusiness, PresentacionBusiness>();

//Email
builder.Services.AddSingleton<IEmailSenderService, EmailSenderService>();


//builder.Services.AddHttpContextAccessor();
//Session
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

//builder.Services.AddSingleton<IPresentacionService, IPresentacionService>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
    options.IdleTimeout = TimeSpan.FromHours(3));

//AUTOMAPPER
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

//DB
builder.Services.Configure<MongoDbSettings>
                          (builder.Configuration.GetSection(nameof(MongoDbSettings)));
builder.Services.AddSingleton<IMongoDbSettings>
                             (d => d.GetRequiredService<IOptions<MongoDbSettings>>().Value);

//builder.Services.AddSingleton<IMongoRepository>(provider =>
//{
//    var mongoClient = new MongoClient("mongodb://localhost:27017");
//    var database = mongoClient.GetDatabase("DeclaracionJurada");
//    return new MongoRepository(database, "Balances");
//});





builder.Services.AddCors(options =>
{
    options.AddPolicy("NuevaPolitica", app =>
    {
        app.AllowAnyOrigin().
        AllowAnyHeader().
        AllowAnyMethod();

    });
}); //Importante 



builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


//Serilog
//app.UseSerilogRequestLogging();

//Cors
app.UseCors("NuevaPolitica");

app.UseSession();

app.UseAuthorization();

app.MapControllers();

app.Run();