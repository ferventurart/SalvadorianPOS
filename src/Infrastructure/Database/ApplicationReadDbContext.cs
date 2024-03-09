using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database;

internal sealed class ApplicationReadDbContext(DbContextOptions<ApplicationReadDbContext> options)
    : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ApplicationReadDbContext).Assembly,
            ReadConfigurationsFilter);
    }

    private static bool ReadConfigurationsFilter(Type type) =>
        type.FullName?.Contains("Configurations.Read") ?? false;
}
