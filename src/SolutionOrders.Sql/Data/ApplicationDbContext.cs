using Microsoft.EntityFrameworkCore;
using SolutionOrders.Core.Persistence;
using SolutionOrders.Sql.Mongo;
using SolutionOrders.Categories.Persistence.EntityConfigurations;
using SolutionOrders.Clients.Persistence.EntityConfigurations;
using SolutionOrders.Items.Persistence.EntityConfigurations;
using SolutionOrders.Orders.Persistence.EntityConfigurations;
using SolutionOrders.UnitOfMeasurements.Persistence.EntityConfigurations;
using SolutionOrders.Workers.Persistence.EntityConfigurations;

namespace SolutionOrders.Sql.Data;

/// <summary>
/// Single SQL Server database context for all bounded contexts; applies EF configurations from each module Persistence assembly.
/// </summary>
public class ApplicationDbContext : DbContext, IUnitOfWork
{
    /// <summary>
    /// Used by <see cref="MongoProjectionSyncInterceptor"/> to relay EF changes pending Mongo upserts for this SaveChanges cycle.
    /// </summary>
    internal PendingMongoSyncBag? PendingMongoProjectionSync { get; set; }

    /// <summary>
    /// Creates the composed DbContext used by all SQL-backed repositories.
    /// </summary>
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply module-specific fluent configurations (one physical database, modular assemblies).
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CategoryConfiguration).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClientConfiguration).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ItemConfiguration).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderConfiguration).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderItemConfiguration).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UnitOfMeasurementConfiguration).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(WorkerConfiguration).Assembly);

        SeedData(modelBuilder);
    }

    private static void SeedData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SolutionOrders.UnitOfMeasurements.Domain.Entities.UnitOfMeasurement>().HasData(
            new SolutionOrders.UnitOfMeasurements.Domain.Entities.UnitOfMeasurement { IdUnitOfMeasurement = 1, Name = "szt", Description = "Sztuki", IsActive = true },
            new SolutionOrders.UnitOfMeasurements.Domain.Entities.UnitOfMeasurement { IdUnitOfMeasurement = 2, Name = "kg", Description = "Kilogramy", IsActive = true },
            new SolutionOrders.UnitOfMeasurements.Domain.Entities.UnitOfMeasurement { IdUnitOfMeasurement = 3, Name = "l", Description = "Litry", IsActive = true });

        modelBuilder.Entity<SolutionOrders.Categories.Domain.Entities.Category>().HasData(
            new SolutionOrders.Categories.Domain.Entities.Category { IdCategory = 1, Name = "Elektronika", Description = "Urządzenia elektroniczne", IsActive = true },
            new SolutionOrders.Categories.Domain.Entities.Category { IdCategory = 2, Name = "Żywność", Description = "Produkty spożywcze", IsActive = true });

        modelBuilder.Entity<SolutionOrders.Clients.Domain.Entities.Client>().HasData(
            new SolutionOrders.Clients.Domain.Entities.Client { IdClient = 1, Name = "Jan Kowalski", Address = "ul. Główna 1, Warszawa", PhoneNumber = "500-100-200", IsActive = true },
            new SolutionOrders.Clients.Domain.Entities.Client { IdClient = 2, Name = "Anna Nowak", Address = "ul. Kwiatowa 5, Kraków", PhoneNumber = "600-200-300", IsActive = true });

        modelBuilder.Entity<SolutionOrders.Workers.Domain.Entities.Worker>().HasData(
            new SolutionOrders.Workers.Domain.Entities.Worker { IdWorker = 1, FirstName = "Piotr", LastName = "Kowalczyk", Login = "pkowalczyk", IsActive = true },
            new SolutionOrders.Workers.Domain.Entities.Worker { IdWorker = 2, FirstName = "Maria", LastName = "Wiśniewska", Login = "mwisnieska", IsActive = true });

        modelBuilder.Entity<SolutionOrders.Items.Domain.Entities.Item>().HasData(
            new SolutionOrders.Items.Domain.Entities.Item { IdItem = 1, Name = "Laptop Dell", Description = "Laptop Dell Inspiron 15", IdCategory = 1, Price = 3500, Quantity = 10, IdUnitOfMeasurement = 1, Code = "LAP001", IsActive = true },
            new SolutionOrders.Items.Domain.Entities.Item { IdItem = 2, Name = "Monitor Samsung", Description = "Monitor 24 cale", IdCategory = 1, Price = 800, Quantity = 15, IdUnitOfMeasurement = 1, Code = "MON001", IsActive = true });
    }
}
