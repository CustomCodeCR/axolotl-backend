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
using Microsoft.Extensions.Hosting;     // IHostEnvironment
using Microsoft.Extensions.Options;    // IOptions<T>
using Newtonsoft.Json;

namespace Axolotl.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
        {
            // 1) Registrar dependencias básicas primero
            services.AddTransient<IOrderingQuery, OrderingQuery>();
            services.AddTransient<IFileStorageService, FileStorageService>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            // UoW: Scoped (recomendado con EF Core)
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Repositorios concretos
            services.AddScoped<IProductPriceHistoryRepository, ProductPriceHistoryRepository>();
            services.AddScoped<IProductStockRepository, ProductStockRepository>();
            services.AddScoped<ISalesRepository, SalesRepository>();
            services.AddScoped<IInvoicesRepository, InvoicesRepository>();
            services.AddScoped<IPaymentsRepository, PaymentsRepository>();
            services.AddScoped<IPaymentMethodsRepository, PaymentMethodsRepository>();

            // Ejemplo: registra tu servicio de secretos (ajusta a tu impl real)
            services.AddSingleton<IVaultSecretService, VaultSecretService>();

            // 2) Bind de ConnectionStrings desde appsettings para Dev/Stage
            services.AddOptions<ConnectionStringsConfig>()
                    .Bind(configuration.GetSection("ConnectionStrings"))
                    .ValidateDataAnnotations()
                    .Validate(o => !string.IsNullOrWhiteSpace(o.Connection),
                              "ConnectionStrings:Connection is required.");

            // 3) Registrar DbContext SIN construir ServiceProvider manual
            services.AddDbContextPool<ApplicationDbContext>((sp, options) =>
            {
                var env = sp.GetRequiredService<IHostEnvironment>();
                string connection;

                if (env.IsProduction())
                {
                    // Lee del Vault en tiempo de ejecución (sin BuildServiceProvider)
                    var secretService = sp.GetRequiredService<IVaultSecretService>();
                    var secretJson = secretService.GetSecret("Axolotl/data/ConnectionStrings").GetAwaiter().GetResult();
                    var secretResponse = JsonConvert.DeserializeObject<SecretResponse<ConnectionStringsConfig>>(secretJson);
                    connection = secretResponse?.Data?.Data?.Connection
                                 ?? throw new InvalidOperationException("Vault secret 'Connection' not found.");
                }
                else
                {
                    // Dev/QA: desde appsettings.{Environment}.json
                    var cfg = sp.GetRequiredService<IOptions<ConnectionStringsConfig>>().Value;
                    connection = cfg.Connection;
                }

                options.UseNpgsql(connection, npgsql =>
                {
                    // Asegura que las migraciones queden en el assembly del DbContext
                    npgsql.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                });
            });

            // 4) Otros servicios de Infra
            services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
            // services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
            // (Opcional DinkToPdf)
            // services.AddSingleton<IConverter, SynchronizedConverter>();

            // AutoMapper (escanea Profiles de Application)
            services.AddAutoMapper(typeof(ProductPriceHistoryProfile).Assembly);

            // MediatR: registra handlers de Application
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(ChangePriceCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(Axolotl.Application.UseCases.Sales.Commands.CreateSale.CreateSaleCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(Axolotl.Application.UseCases.Invoices.Commands.CreateFromSale.CreateInvoiceFromSaleCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(Axolotl.Application.UseCases.Payments.Commands.CapturePayment.CapturePaymentCommand).Assembly);
            });

            return services;
        }
    }
}
