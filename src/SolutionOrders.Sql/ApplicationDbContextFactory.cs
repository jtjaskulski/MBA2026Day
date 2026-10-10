using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using SolutionOrders.Sql.Data;

namespace SolutionOrders.Sql;

/// <summary>
/// Allows EF Core CLI (<c>dotnet ef</c>) to create <see cref="ApplicationDbContext"/> without starting the API.
/// </summary>
public sealed class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    /// <inheritdoc />
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseSqlServer(
            "Server=(localdb)\\mssqllocaldb;Database=SolutionOrders_Dev;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True");
        return new ApplicationDbContext(optionsBuilder.Options);
    }
}
