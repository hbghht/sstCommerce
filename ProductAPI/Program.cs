using ProductAPI.Settings;
using ProductAPI.Data;
using ProductAPI.Commands;
using ProductAPI.Repositories;
using Microsoft.Extensions.Options;
using System.Text.Json.Serialization;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMassTransit(x =>
{
    x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(config =>
    {
        config.Host(new Uri(builder.Configuration["EventBus:HostName"]), h =>
        {
            h.Username(builder.Configuration["EventBus:UserName"]);
            h.Password(builder.Configuration["EventBus:Password"]);
        });
    }));
});

builder.Services.AddMassTransitHostedService();
builder.Services.AddControllers();
builder.Services.Configure<EventBus>(builder.Configuration.GetSection(nameof(EventBus)));
builder.Services.AddSingleton<EventBus>(sp => sp.GetRequiredService<IOptions<EventBus>>().Value);
builder.Services.Configure<ProductDatabaseSettings>(builder.Configuration.GetSection(nameof(ProductDatabaseSettings)));
builder.Services.AddSingleton<ProductDatabaseSettings>(sp => sp.GetRequiredService<IOptions<ProductDatabaseSettings>>().Value);
builder.Services.AddTransient<IProductContext, ProductContext>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<SearchCommand>();
builder.Services.AddTransient<FilterByColorCommand>();
builder.Services.AddTransient<FilterByBranchCommand>();
builder.Services.AddTransient<IViewCommand, ViewCommand>();
builder.Services.AddTransient<Func<CommandEnum, ProductColumnEnum?, IQueryCommand>>((serviceProvider =>(key, filterBy) =>  
{  
    switch (key)  
    {  
        case CommandEnum.SEARCH:  
            return serviceProvider.GetService<SearchCommand>();  
        default:  
            switch (filterBy)
            {
                case ProductColumnEnum.Color:
                    return serviceProvider.GetService<FilterByColorCommand>();
                default:
                    return serviceProvider.GetService<FilterByBranchCommand>();
            }
    }  
})  );

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.EnableAnnotations();
    c.DescribeAllParametersInCamelCase();
    });
builder.Services.AddControllersWithViews()
                .AddJsonOptions(options => 
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

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
