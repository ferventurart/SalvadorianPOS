using Application.Abstractions.Caching;
using Application.Abstractions.Data;
using Application.Abstractions.Notifications;
using Domain.StoreDepartments;
using Infrastructure.Caching;
using Infrastructure.Database;
using Infrastructure.Health;
using Infrastructure.Notifications;
using Infrastructure.Outbox;
using Infrastructure.Repositories;
using Infrastructure.Time;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Npgsql;
using SharedKernel;

namespace Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMediatR(config =>
            config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        string? connectionString = configuration.GetConnectionString("Database");
        Ensure.NotNullOrEmpty(connectionString);

        services.AddSingleton(_ =>
            new DbConnectionFactory(new NpgsqlDataSourceBuilder(connectionString).Build()));

        services.AddDbContext<ApplicationReadDbContext>(
            options => options
                .UseNpgsql(connectionString)
                .UseSnakeCaseNamingConvention()
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

        services.AddSingleton<PublishDomainEventsInterceptor>();
        services.AddSingleton<InsertOutboxMessagesInterceptor>();

        services.AddDbContext<ApplicationWriteDbContext>(
            (sp, options) => options
                .UseNpgsql(connectionString)
                .UseSnakeCaseNamingConvention()
                .AddInterceptors(sp.GetRequiredService<InsertOutboxMessagesInterceptor>()));

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationWriteDbContext>());
        
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationWriteDbContext>());
        
        services.AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<ApplicationReadDbContext>());
        
        services.AddTransient<IStoreDepartmentRepository, StoreDepartmentRepository>();

        string redisConnectionString = configuration.GetConnectionString("Cache")!;

        services.AddStackExchangeRedisCache(options =>
            options.Configuration = redisConnectionString);

        services.AddSingleton<ICacheService, CacheService>();

        services.AddHealthChecks()
            .AddNpgSql(connectionString)
            .AddRedis(redisConnectionString);
    }
}
