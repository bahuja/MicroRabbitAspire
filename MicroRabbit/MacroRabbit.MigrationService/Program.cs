using MicroRabbit.Banking.Data.Context;
using MicroRabbit.MigrationService;
using MicroRabbit.TransferData.Context;
var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddHostedService<Worker>();

builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing.AddSource(Worker.ActivitySourceName));

builder.AddSqlServerDbContext<BankingDbContext>("BankingDb");

builder.AddSqlServerDbContext<TransferDbContext>("TransferDb");
var host = builder.Build();
host.Run();
