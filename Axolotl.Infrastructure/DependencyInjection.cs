using Axolotl.Application.Commons.Config;
using Axolotl.Application.Interfaces.Persistence;
using Axolotl.Application.Interfaces.Services;
using Axolotl.Infrastructure.Authentication;
using Axolotl.Infrastructure.Persistence;
using Axolotl.Infrastructure.Persistence.Context;
using Axolotl.Infrastructure.Persistence.Repositories;
using Axolotl.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace Axolotl.Infrastructure;

public static class DependencyInjection
{
    // Usa IConfiguration (o ConfigurationManager) y resuelve todo dentro del factory del DbContext
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // DbContext con factory que conoce el entorno y puede pedir Vault solo en producción
        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            var env = sp.GetRequiredService<IHostEnvironment>();
            string cs;

            if (!env.IsProduction())
            {
                cs = configuration.GetConnectionString("Connection")
                     ?? throw new InvalidOperationException("Missing connection string 'Connection'.");
            }
            else
            {
                // En Program.cs ya registraste IVaultSecretService cuando es producción
                var secretService = sp.GetRequiredService<IVaultSecretService>();
                var secretJson = secretService.GetSecret("Axolotl/data/ConnectionStrings")
                                                 .GetAwaiter().GetResult();

                var secretResponse = JsonConvert.DeserializeObject<SecretResponse<ConnectionStringsConfig>>(secretJson);
                var cfg = secretResponse?.Data?.Data
                         ?? throw new InvalidOperationException("Vault: ConnectionStrings not found.");
                cs = cfg.Connection
                     ?? throw new InvalidOperationException("Vault: 'Connection' value is null.");
            }

            options.UseNpgsql(cs);
        });

        
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IInventoryMovementRepository, InventoryMovementRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddTransient<IOrderingQuery, OrderingQuery>();
        services.AddTransient<IFileStorageService, FileStorageService>();
        services.AddScoped<IPurchaseOrderRepository, PurchaseOrderRepository>();
        services.AddScoped<ISupplierInvoiceRepository, SupplierInvoiceRepository>();
        services.AddScoped<ICartRepository, CartRepository>();
        services.AddScoped<ISupplierInvoiceRepository, SupplierInvoiceRepository>();


        // JWT (si lo necesitas aquí)
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));

        return services;
    }
}
