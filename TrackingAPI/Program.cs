using EventBusRabbitMQ.Common;
using MassTransit;
using TrackingAPI.Consumers;
using GreenPipes;
using Microsoft.Extensions.Options;
using TrackingAPI.Settings;
using TrackingAPI.Repositories;
using TrackingAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<ProductSearchConsumer>();
    x.AddConsumer<ProductFilterConsumer>();
    x.AddConsumer<ProductViewConsumer>();
    x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
    {
        cfg.Host(new Uri(builder.Configuration["EventBus:HostName"]), h =>
        {
            h.Username(builder.Configuration["EventBus:UserName"]);
            h.Password(builder.Configuration["EventBus:Password"]);
        });
        cfg.ReceiveEndpoint(EventBusConstants.ProductSearchQueue, ep =>
        {
            ep.PrefetchCount = 16;
            ep.UseMessageRetry(r => r.Interval(2, 100));
            ep.ConfigureConsumer<ProductSearchConsumer>(provider);
        });
        cfg.ReceiveEndpoint(EventBusConstants.ProductFilterQueue, ep =>
        {
            ep.PrefetchCount = 16;
            ep.UseMessageRetry(r => r.Interval(2, 100));
            ep.ConfigureConsumer<ProductFilterConsumer>(provider);
        });
        cfg.ReceiveEndpoint(EventBusConstants.ProductViewQueue, ep =>
        {
            ep.PrefetchCount = 16;
            ep.UseMessageRetry(r => r.Interval(2, 100));
            ep.ConfigureConsumer<ProductViewConsumer>(provider);
        });
    }));
});

builder.Services.AddMassTransitHostedService();

builder.Services.AddControllers();

builder.Services.Configure<TrackingDatabaseSettings>(builder.Configuration.GetSection(nameof(TrackingDatabaseSettings)));
builder.Services.AddSingleton<TrackingDatabaseSettings>(sp => sp.GetRequiredService<IOptions<TrackingDatabaseSettings>>().Value);
builder.Services.AddTransient<ISearchHistoryRepository, SearchHistoryRepository>();
builder.Services.AddTransient<IFilterHistoryRepository, FilterHistoryRepository>();
builder.Services.AddTransient<IViewHistoryRepository, ViewHistoryRepository>();
builder.Services.AddTransient<ITrackingContext, TrackingContext>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
