using Basket.Api.Data;
using Basket.Api.Models;
using BuildingBlocks.Behaviors;
using BuildingBlocks.Exceptions.Handler;
using Carter;
using HealthChecks.UI.Client;
using Marten;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCarter();

builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.Decorate<IBasketRepository, CachedBasketRepository>();
builder.Services.AddStackExchangeRedisCache(opt =>
{
	opt.Configuration = builder.Configuration.GetConnectionString("Redis");
});

builder.Services.AddExceptionHandler<CustomExceptionHandler>();
builder
	   .Services
	   .AddHealthChecks()
	   .AddNpgSql(builder.Configuration.GetConnectionString("Database")!)
	   .AddRedis(builder.Configuration.GetConnectionString("Redis")!);
var assembly = typeof(Program).Assembly;

builder.Services.AddMediatR(config =>
{
	config.RegisterServicesFromAssembly(assembly);
	config.AddOpenBehavior(typeof(ValidationBehavior<,>));
	config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});
builder.Services.AddMarten(opt =>
{
	opt.Connection(builder.Configuration.GetConnectionString("Database")!);
	opt.Schema.For<ShoppingCart>().Identity(x => x.UserName);
}).UseLightweightSessions();

builder.Services.AddHealthChecks();

var app = builder.Build();
app.MapCarter();
app.UseExceptionHandler(opt =>
{

});
app.UseHealthChecks("/health", new HealthCheckOptions
{
	ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
app.Run();
