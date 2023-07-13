using EventBus.Message.Common;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Ordering.API.EventBusConsumer;
using Ordering.API.Extensions;
using Ordering.API.Mapping;
using Ordering.Application.Features.Orders.Queries.GetOrderList;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

// MassTransit-RabbitMQ Configuration
builder.Services.AddMassTransit(config => {

    config.AddConsumer<BasketCheckoutConsumer>();

    config.UsingRabbitMq((ctx, cfg) => {
        cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);
        //cfg.UseHealthCheck(ctx);

        cfg.ReceiveEndpoint(EventBusConstants.BasketCheckoutQueue, c => {
            c.ConfigureConsumer<BasketCheckoutConsumer>(ctx);
        });
    });
});
//builder.Services.AddMassTransitHostedService();

// General Configuration
builder.Services.AddScoped<BasketCheckoutConsumer>();
builder.Services.AddAutoMapper(typeof(OrderingProfile));
builder.Services.AddMediatR(typeof(GetOrdersListQuery).GetTypeInfo().Assembly);


builder.Services.AddControllers();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
