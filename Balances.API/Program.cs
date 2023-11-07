using Balances.Repository.Contract;
using Balances.Repository.Implementation;
using Balances.Utilities;
using Balances.Services.Contract;
using Balances.Services.Implementation;
using MongoDB.Driver;
using Dominio.Helpers;
using EmailSender;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));

builder.Services.AddSingleton<IBalanceService, BalanceService>();
builder.Services.AddSingleton<IEmailSenderService, EmailSenderService>();


builder.Services.AddSingleton<IMongoRepository>(provider =>
{
    var mongoClient = new MongoClient("mongodb://localhost:27017");
    var database = mongoClient.GetDatabase("DeclaracionJurada");
    return new MongoRepository(database, "Balances");
});




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

app.UseCors("NuevaPolitica");

app.UseAuthorization();

app.MapControllers();

app.Run();
