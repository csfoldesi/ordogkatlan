﻿using Application.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddSingleton(TimeProvider.System);

        var connectionString = configuration.GetConnectionString("MysqlConnection")!;
        var severVersion = ServerVersion.AutoDetect(connectionString);
        services.AddDbContext<DataContext>(
            (serviceProvider, options) =>
                options
                    .UseMySql(connectionString, severVersion)
                    .LogTo(Console.WriteLine, LogLevel.Information)
        );

        services.AddScoped<IDataContext>(provider => provider.GetRequiredService<DataContext>());

        return services;
    }
}
