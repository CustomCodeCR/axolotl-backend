using Axolotl.Application.Commons.Config;
using Axolotl.Application.Interfaces.Services;
using Newtonsoft.Json;

namespace Axolotl.Api.Middleware;

public static class HealthCheckExtension
{
    private static readonly string[] DatabaseTags = { "database" };

    public static IServiceCollection AddHealthCheck(this IServiceCollection services, IConfiguration configuration)
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        string connectionString;

        if (environment != "Production")
        {
            connectionString = configuration.GetConnectionString("Connection")!;
        }
        else
        {
            var serviceProvider = services.BuildServiceProvider();
            var vaultSecretService = serviceProvider.GetRequiredService<IVaultSecretService>();

            var secretJson = vaultSecretService.GetSecret("VetFriends/data/ConnectionStrings").GetAwaiter().GetResult();
            var secretResponse = JsonConvert.DeserializeObject<SecretResponse<ConnectionStringsConfig>>(secretJson);

            if (secretResponse?.Data?.Data == null || string.IsNullOrEmpty(secretResponse.Data.Data.Connection))
            {
                throw new Exception("The connection string could not be obtained from Vault.");
            }

            connectionString = secretResponse.Data.Data.Connection;
        }

        services.AddHealthChecks()
            .AddNpgSql(
                connectionString,
                tags: DatabaseTags);

        services.AddHealthChecksUI()
            .AddInMemoryStorage();

        return services;
    }
}