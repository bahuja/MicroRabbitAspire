using MicroRabbit.Banking.Data.Context;
using MicroRabbit.Domain.Core.Bus;
using MicroRabbit.Infra.IoC;
using MicroRabbit.Transfer.Domain.EventHandlers;
using MicroRabbit.Transfer.Domain.Events;
using MicroRabbit.TransferData.Context;
using RabbitMQ.Client;

//using Microsoft.EntityFrameworkCore;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
//builder.Services.AddDbContext<TransferDbContext>(options =>
//{
//    options.UseSqlServer("TransferDb");
//});
builder.AddSqlServerDbContext<TransferDbContext>("TransferDb");
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.AddRabbitMQClient("eventbus", configureConnectionFactory: factory =>
{
    ((ConnectionFactory)factory).DispatchConsumersAsync = true;
});
RegisterServices(builder.Services);
var app = builder.Build();

var eventBus = app.Services.GetRequiredService<IEventBus>();
eventBus.Subscribe<TransferCreatedEvent, TransferEventHandler>();

//ConfigureEventBus(app);
app.MapDefaultEndpoints();

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
void RegisterServices(IServiceCollection services)
{
    DependencyContainer.RegisterServices(services);
}
void ConfigureEventBus(IApplicationBuilder app)
{
    var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
    eventBus.Subscribe<TransferCreatedEvent, TransferEventHandler>();
}