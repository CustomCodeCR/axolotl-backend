using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Axolotl.Application.Commons.Config;      // ConnectionStringsConfig (if you use it)
using Axolotl.Application.Interfaces.Services; // IVaultSecretService
using Axolotl.Infrastructure.Services;         // VaultSecretService
using Newtonsoft.Json;

namespace Axolotl.Infrastructure.Persistence.Context
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            // Build configuration from the startup project's directory
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
                .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: false)
                .AddEnvironmentVariables() // kept for Vault settings if your service reads from ENV
                .Build();

            var isProduction = string.Equals(environment, "Production", StringComparison.OrdinalIgnoreCase);

            string? conn = null;

            if (!isProduction)
            {
                // Development path: read from appsettings.*.json
                conn = configuration.GetConnectionString("Connection")
                       ?? configuration.GetConnectionString("DefaultConnection");

                if (string.IsNullOrWhiteSpace(conn))
                    throw new InvalidOperationException("Connection string not found in appsettings for Development.");
            }
            else
            {
                // Production path: resolve from Vault
                var services = new ServiceCollection();

                // Provide IConfiguration so your Vault service can read VAULT_* or other settings if needed
                services.AddSingleton<IConfiguration>(configuration);
                services.AddLogging(b => b.AddConsole());
                services.AddSingleton<IVaultSecretService, VaultSecretService>();

                using var sp = services.BuildServiceProvider();
                var vault = sp.GetRequiredService<IVaultSecretService>();

                var secretJson = vault.GetSecret("Axolotl/data/ConnectionStrings").GetAwaiter().GetResult();
                var secretResponse = JsonConvert.DeserializeObject<SecretResponse<ConnectionStringsConfig>>(secretJson);
                conn = secretResponse?.Data?.Data?.Connection;

                if (string.IsNullOrWhiteSpace(conn))
                    throw new InvalidOperationException("Vault did not return a valid connection string at 'Axolotl/data/ConnectionStrings'.");
            }

            // Helpful Npgsql flags during design-time
            if (!conn.Contains("Include Error Detail", StringComparison.OrdinalIgnoreCase))
                conn += ";Include Error Detail=true";
            if (!conn.Contains("Trust Server Certificate", StringComparison.OrdinalIgnoreCase))
                conn += ";Trust Server Certificate=true";

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseNpgsql(conn, npgsql =>
            {
                // Ensure migrations are placed in the same assembly as the DbContext
                npgsql.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
            });

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}