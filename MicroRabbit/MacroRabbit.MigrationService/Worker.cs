using System.Diagnostics;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

using OpenTelemetry.Trace;


using MicroRabbit.Transfer.Domain.Models;
using MicroRabbit.TransferData.Context;
using MicroRabbit.Banking.Domain.Models;
using MicroRabbit.Banking.Data.Context;


namespace MicroRabbit.MigrationService;

public class Worker(
    IServiceProvider serviceProvider,
    IHostApplicationLifetime hostApplicationLifetime) : BackgroundService
{
    public const string ActivitySourceName = "Migrations";
    private static readonly ActivitySource s_activitySource = new(ActivitySourceName);

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        using var activity = s_activitySource.StartActivity("Migrating database", ActivityKind.Client);

        try
        {
            using var scope = serviceProvider.CreateScope();
            var transferDbContext = scope.ServiceProvider.GetRequiredService<TransferDbContext>();

            await EnsureDatabaseAsync(transferDbContext, cancellationToken);
            await RunMigrationAsync(transferDbContext, cancellationToken);
            await SeedDataAsync(transferDbContext, cancellationToken);

            var bankingDbContext = scope.ServiceProvider.GetRequiredService<BankingDbContext>();

            await EnsureDatabaseAsync(bankingDbContext, cancellationToken);
            await RunMigrationAsync(bankingDbContext, cancellationToken);
            await SeedDataAsync(bankingDbContext, cancellationToken);


        }
        catch (Exception ex)
        {
            activity?.RecordException(ex);
            throw;
        }

        hostApplicationLifetime.StopApplication();
    }

    private static async Task EnsureDatabaseAsync(TransferData.Context.TransferDbContext dbContext, CancellationToken cancellationToken)
    {
        var dbCreator = dbContext.GetService<IRelationalDatabaseCreator>();

        var strategy = dbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            // Create the database if it does not exist.
            // Do this first so there is then a database to start a transaction against.
            if (!await dbCreator.ExistsAsync(cancellationToken))
            {
                await dbCreator.CreateAsync(cancellationToken);
            }
        });
    }

    private static async Task RunMigrationAsync(TransferData.Context.TransferDbContext dbContext, CancellationToken cancellationToken)
    {
        var strategy = dbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            // Run migration in a transaction to avoid partial migration if it fails.
            await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
            await dbContext.Database.MigrateAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        });
    }

    private static async Task SeedDataAsync(TransferData.Context.TransferDbContext dbContext, CancellationToken cancellationToken)
    {
        TransferLog firstLog = new()
        {
            FromAccount = 1,
            ToAccount = 2,
            TransferAmount = 10
        };
        TransferLog second = new()
        {
            FromAccount = 2,
            ToAccount = 1,
            TransferAmount = 5
        };


        var strategy = dbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            // Seed the database
            await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
            await dbContext.TransferLogs.AddAsync(firstLog, cancellationToken);
            await dbContext.TransferLogs.AddAsync(second, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        });
    }

    private static async Task EnsureDatabaseAsync(BankingDbContext dbContext, CancellationToken cancellationToken)
    {
        var dbCreator = dbContext.GetService<IRelationalDatabaseCreator>();

        var strategy = dbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            // Create the database if it does not exist.
            // Do this first so there is then a database to start a transaction against.
            if (!await dbCreator.ExistsAsync(cancellationToken))
            {
                await dbCreator.CreateAsync(cancellationToken);
            }
        });
    }

    private static async Task RunMigrationAsync(BankingDbContext dbContext, CancellationToken cancellationToken)
    {
        var strategy = dbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            // Run migration in a transaction to avoid partial migration if it fails.
            await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
            await dbContext.Database.MigrateAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        });
    }

    private static async Task SeedDataAsync(BankingDbContext dbContext, CancellationToken cancellationToken)
    {
        Account firstAccount = new()
        {
            
            AccountBalance = 50,
            AccountType = "Saving",
        };
        Account second = new()
        {

            AccountBalance = 100,
            AccountType = "Saving",
        };
        Account third = new()
        {

            AccountBalance = 150,
            AccountType = "Saving",
        };
        var strategy = dbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            // Seed the database
            await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
            await dbContext.Accounts.AddAsync(firstAccount, cancellationToken);
            await dbContext.Accounts.AddAsync(second, cancellationToken);
            await dbContext.Accounts.AddAsync(third, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        });
    }


}
