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


//SMTP Settings
builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));

//Balance
builder.Services.AddSingleton<IBalanceService, BalanceService>();
builder.Services.AddScoped<IContadorService, ContadorService>();
builder.Services.AddScoped<IEstadoContableService, EstadoContableService>();
//builder.Services.AddScoped<IRepresentanteLegalService, RepresentanteLegalService>();
builder.Services.AddScoped<IArchivoService, ArchivoService>();
//builder.Services.AddScoped<IPresentacionService, PresentacionService>();

//Email
builder.Services.AddSingleton<IEmailSenderService, EmailSenderService>();

//Session
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<ISessionService, SessionService>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
    options.IdleTimeout = TimeSpan.FromHours(3));

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

//Session
app.UseSession();

//Cors
app.UseCors("NuevaPolitica");

app.UseAuthorization();

app.MapControllers();

app.Run();
