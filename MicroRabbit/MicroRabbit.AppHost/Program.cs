var builder = DistributedApplication.CreateBuilder(args);

var rabbitMq = builder.AddRabbitMQ("eventbus");
var sql = builder.AddSqlServer("sql");
var TransferDb = sql.AddDatabase("TransferDb");
var BankingDb = sql.AddDatabase("BankingDb");

builder.AddProject<Projects.MacroRabbit_MigrationService>("macrorabbit-migrationservice")
    .WithReference(TransferDb)
    .WithReference(BankingDb);

var transferApi = builder.AddProject<Projects.MicroRabbit_Transfer_ApiNew>("microrabbit-transfer-apinew")
    .WithReference(TransferDb)
    .WithReference(rabbitMq);

var bankingApi = builder.AddProject<Projects.MicroRabbit_Banking_ApiNew>("microrabbit-banking-apinew")
    .WithReference(BankingDb)
    .WithReference(rabbitMq);

var transferEndPoint = transferApi.GetEndpoint("https");
var bankingEndPoint = bankingApi.GetEndpoint("https");

builder.AddProject<Projects.MicroRabbit_MvcNew>("microrabbit-mvcnew")
    .WithEnvironment("BANKING_ENDPOINT", bankingEndPoint)
    .WithEnvironment("TRANSFER_ENDPOINT", transferEndPoint);



builder.Build().Run();
