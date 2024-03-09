using System.Data;
using Dapper;
using Infrastructure.Database;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Infrastructure.Health;

internal sealed class DatabaseHealthCheck(DbConnectionFactory dbConnectionFactory) : IHealthCheck
{
    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        try
        {
            using IDbConnection connection = dbConnectionFactory.OpenConnection();

            await connection.ExecuteScalarAsync("SELECT 1");

            return HealthCheckResult.Healthy();
        }
        catch (Exception e)
        {
            return HealthCheckResult.Unhealthy(exception: e);
        }
    }
}
