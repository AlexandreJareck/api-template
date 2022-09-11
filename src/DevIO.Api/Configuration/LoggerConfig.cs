using DevIO.Api.Extensions;

namespace DevIO.Api.Configuration;

public static class LoggerConfig
{
    public static IServiceCollection AddLoggingConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHealthChecks()
                .AddCheck("Products", new SqlServerHealthCheck(configuration.GetConnectionString("DefaultConnection")))
                .AddSqlServer(configuration.GetConnectionString("DefaultConnection"), name: "BancoSQL");

            services.AddHealthChecksUI()
                .AddSqlServerStorage(configuration.GetConnectionString("DefaultConnection"));

            return services;
    }
}