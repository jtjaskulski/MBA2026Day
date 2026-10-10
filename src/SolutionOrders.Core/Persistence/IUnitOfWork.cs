namespace SolutionOrders.Core.Persistence;

/// <summary>
/// Unit-of-work abstraction so Application handlers do not reference EF Core or SQL types directly.
/// Implemented by the composed <c>ApplicationDbContext</c> in the Sql host project.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Persists tracked changes to SQL Server (source of truth).
    /// </summary>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
