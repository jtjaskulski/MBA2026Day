using Mapster;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using SolutionOrders.Categories.Application.Handlers.Queries;
using SolutionOrders.Categories.Infrastructure;
using SolutionOrders.Clients.Infrastructure;
using SolutionOrders.Items.Infrastructure;
using SolutionOrders.Orders.Infrastructure;
using SolutionOrders.Sql.Data;
using SolutionOrders.UnitOfMeasurements.Infrastructure;
using SolutionOrders.Workers.Infrastructure;
using SolutionOrders.Core.Persistence;

namespace SolutionOrders.API;

/// <summary>
/// HTTP API composition root: registers modular Clean Architecture slices, SQL (writes), and MongoDB (read projections).
/// </summary>
public class Program
{
    /// <summary>
    /// Application entry point.
    /// </summary>
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        InitializeServicesAndDbContext(builder);
        SetUpCorsPolicyForDevelopment(builder);

        var app = builder.Build();
        InitializeAutomaticMigrations(app);
        InitializeDevelopmentEnvironment(app);

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }

    private static void SetUpCorsPolicyForDevelopment(WebApplicationBuilder builder)
    {
        if (builder.Environment.IsDevelopment())
        {
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    policy => policy
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });
        }
    }

    private static void InitializeServicesAndDbContext(WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        builder.Services.AddOpenApi();

        var sqlConnection = builder.Configuration.GetConnectionString("ApplicationDbContext")
            ?? throw new InvalidOperationException("Connection string 'ApplicationDbContext' is not configured.");

        var mongoConn = builder.Configuration.GetConnectionString("MongoDb")
            ?? "mongodb://127.0.0.1:27017";
        var mongoDbName = builder.Configuration["MongoDb:DatabaseName"] ?? "SolutionOrdersRead";

        builder.Services.AddSingleton<IMongoClient>(_ => new MongoClient(mongoConn));
        builder.Services.AddSingleton(sp =>
            sp.GetRequiredService<IMongoClient>().GetDatabase(mongoDbName));

        builder.Services.AddSingleton<SolutionOrders.Sql.Mongo.MongoProjectionSyncService>();
        builder.Services.AddSingleton<SolutionOrders.Sql.Mongo.MongoProjectionSyncInterceptor>();

        builder.Services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
            options.UseSqlServer(sqlConnection)
                .AddInterceptors(serviceProvider.GetRequiredService<SolutionOrders.Sql.Mongo.MongoProjectionSyncInterceptor>()));

        // Same DbContext instance serves EF commands and implements IUnitOfWork for Application handlers.
        builder.Services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());
        builder.Services.AddScoped<DbContext>(sp => sp.GetRequiredService<ApplicationDbContext>());

        var applicationAssemblies = new[]
        {
            typeof(GetAllCategoriesHandler).Assembly,
            typeof(SolutionOrders.Clients.Application.Handlers.Queries.GetAllClientsHandler).Assembly,
            typeof(SolutionOrders.Items.Application.Handlers.Queries.GetAllItemsHandler).Assembly,
            typeof(SolutionOrders.Orders.Application.Handlers.Queries.GetAllOrdersHandler).Assembly,
            typeof(SolutionOrders.UnitOfMeasurements.Application.Handlers.Queries.GetAllUnitOfMeasurementsHandler).Assembly,
            typeof(SolutionOrders.Workers.Application.Handlers.Queries.GetAllWorkersHandler).Assembly,
        };

        builder.Services.AddMediatR(cfg =>
        {
            foreach (var assembly in applicationAssemblies)
                cfg.RegisterServicesFromAssembly(assembly);
        });

        builder.Services.AddCategoriesModule();
        builder.Services.AddClientsModule();
        builder.Services.AddItemsModule();
        builder.Services.AddOrdersModule();
        builder.Services.AddUnitOfMeasurementsModule();
        builder.Services.AddWorkersModule();

        // Mapster mapping configs live inside each Application assembly.
        foreach (var assembly in applicationAssemblies)
            TypeAdapterConfig.GlobalSettings.Scan(assembly);

        builder.Services.AddHostedService<SolutionOrders.Sql.Mongo.MongoProjectionBackfillHostedService>();
    }

    private static void InitializeDevelopmentEnvironment(WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseCors("AllowAll");
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/openapi/v1.json", "v1");
            });
        }
    }

    private static void InitializeAutomaticMigrations(WebApplication app)
    {
        const int retryCount = 5;
        using var scope = app.Services.CreateScope();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        // No embedded migrations yet → Dev can bootstrap schema with EnsureCreated; Production must ship migrations.
        var migrationsKnownToModel = dbContext.Database.GetMigrations().ToList();
        if (migrationsKnownToModel.Count == 0)
        {
            if (!app.Environment.IsDevelopment())
            {
                throw new InvalidOperationException(
                    "No EF Core migrations are present in the SolutionOrders.Sql assembly. Generate migrations before deployment (see README).");
            }

            logger.LogWarning(
                "No EF Core migrations found — using EnsureCreated() for Development only. After adding migrations, drop/recreate the database or switch to Migrate().");

            Exception? ensureLast = null;
            for (var i = 0; i < retryCount; i++)
            {
                try
                {
                    dbContext.Database.EnsureCreated();
                    logger.LogInformation("Development database schema created via EnsureCreated().");
                    return;
                }
                catch (Exception ex)
                {
                    ensureLast = ex;
                    logger.LogWarning(ex, "EnsureCreated attempt {Attempt}/{MaxRetries} failed. Retrying in 5s...", i + 1, retryCount);
                    Thread.Sleep(5000);
                }
            }

            throw new InvalidOperationException("EnsureCreated failed after retries.", ensureLast);
        }

        Exception? migrateLast = null;
        for (var i = 0; i < retryCount; i++)
        {
            try
            {
                dbContext.Database.Migrate();
                logger.LogInformation("Database migrations applied successfully.");
                return;
            }
            catch (Exception ex)
            {
                migrateLast = ex;
                logger.LogWarning(ex, "Migration attempt {Attempt}/{MaxRetries} failed. Retrying in 5s...", i + 1, retryCount);
                Thread.Sleep(5000);
            }
        }

        logger.LogError("Could not apply migrations after {MaxRetries} attempts.", retryCount);
        throw new InvalidOperationException("Failed to apply database migrations after multiple attempts. Aborting application startup.", migrateLast);
    }
}
