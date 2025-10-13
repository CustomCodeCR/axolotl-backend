using Axolotl.Application.Commons.Config;
using Axolotl.Application.Interfaces.Persistence;
using Axolotl.Application.Interfaces.Services;
using Axolotl.Application.Mappings;
using Axolotl.Application.UseCases.ProductPriceHistory.Commands.ChangePrice;
using Axolotl.Infrastructure.Authentication;
using Axolotl.Infrastructure.Persistence.Context;
using Axolotl.Infrastructure.Persistence.Repositories;
using Axolotl.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;


namespace Axolotl.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
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
            var secretService = serviceProvider.GetRequiredService<IVaultSecretService>();

            var secretJson = secretService.GetSecret("Axolotl/data/ConnectionStrings").GetAwaiter().GetResult();
            var secretResponse = JsonConvert.DeserializeObject<SecretResponse<ConnectionStringsConfig>>(secretJson);
            var config = secretResponse?.Data?.Data;
            connectionString = config!.Connection;
        }

        services.AddSingleton(connectionString);

        services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
        {
            var resolvedConnection = serviceProvider.GetRequiredService<string>();
            options.UseNpgsql(resolvedConnection);
        });

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        services.AddTransient<IOrderingQuery, OrderingQuery>();
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        services.AddTransient<IFileStorageService, FileStorageService>();

        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
        //services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        return services;
    }
}