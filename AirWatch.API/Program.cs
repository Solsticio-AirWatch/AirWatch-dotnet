using AirWatch.Application.Services;
using AirWatch.Infrastructure.Persistence;
using AirWatch.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AirWatchContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ISensorRepository, SensorRepository>();
builder.Services.AddScoped<IAirReadingRepository, AirReadingRepository>();
builder.Services.AddScoped<IAlertConfigRepository, AlertConfigRepository>();
builder.Services.AddScoped<IAlertEventRepository, AlertEventRepository>();
builder.Services.AddScoped<IIntegrationLogRepository, IntegrationLogRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new()
    {
        Title = "AirWatch API",
        Version = "v1",
        Description = "API REST para monitoramento de qualidade do ar — Grupo Solstício FIAP 2026"
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{ 
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "AirWatch API v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
