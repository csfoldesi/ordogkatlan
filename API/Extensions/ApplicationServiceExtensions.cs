using System.Reflection;
using System.Text.Json.Serialization;
using API.Services;
using Application.Core.Interfaces;

namespace API.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddAPIServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services
            .AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.WriteIndented = true; // Optional: pretty print
            });
        services.AddEndpointsApiExplorer();
        services.AddScoped<IUser, CurrentUser>();
        services.AddOpenApi();

        services.AddHttpContextAccessor();
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddCors(options =>
        {
            options.AddPolicy(
                "CorsPolicy",
                policy =>
                {
                    policy
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                        .WithOrigins(
                            [.. configuration.GetSection("AllowedOrigins")!.Get<List<string>>()!]
                        );
                }
            );
        });

        return services;
    }
}
