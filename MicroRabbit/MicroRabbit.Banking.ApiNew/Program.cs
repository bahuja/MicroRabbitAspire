using MicroRabbit.Banking.Data.Context;
using MicroRabbit.Infra.IoC;
using RabbitMQ.Client;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddSqlServerDbContext<BankingDbContext>("BankingDb");

// Add services to the container.

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
//void ConfigureEventBus(IApplicationBuilder app)
//{
//    var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
//    eventBus.Publish<TransferCreatedEvent, TransferEventHandler>();
//}

