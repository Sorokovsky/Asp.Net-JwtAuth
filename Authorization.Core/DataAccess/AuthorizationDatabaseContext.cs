using Authorization.Core.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Authorization.Core.DataAccess;

public class AuthorizationDatabaseContext : DbContext
{
    private readonly IConfiguration _configuration;

    public AuthorizationDatabaseContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public DbSet<UserEntity> Users => Set<UserEntity>();

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateUpdatedAt();
        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseNpgsql(_configuration.GetConnectionString(nameof(AuthorizationDatabaseContext))!)
            .UseSnakeCaseNamingConvention();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AuthorizationDatabaseContext).Assembly);
    }

    private void UpdateUpdatedAt()
    {
        var updatedEntries = ChangeTracker.Entries<BaseEntity>()
            .Where(entity => entity.State == EntityState.Modified);
        foreach (var entry in updatedEntries)
        {
            entry.Entity.UpdatedAt = DateTime.UtcNow;
        }
    }
}