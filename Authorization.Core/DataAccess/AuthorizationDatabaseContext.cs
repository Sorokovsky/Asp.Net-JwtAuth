using Microsoft.EntityFrameworkCore;

namespace Authorization.Core.DataAccess;

public class AuthorizationDatabaseContext : DbContext
{
    private readonly IConfiguration _configuration;

    public AuthorizationDatabaseContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseNpgsql(_configuration.GetConnectionString(nameof(AuthorizationDatabaseContext))!)
            .UseSnakeCaseNamingConvention();
    }
}