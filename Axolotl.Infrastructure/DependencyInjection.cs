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
using Newtonsoft.Json;

namespace Axolotl.Infrastructure
{
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
                var sp = services.BuildServiceProvider();
                var secretService = sp.GetRequiredService<IVaultSecretService>();

                var secretJson = secretService.GetSecret("Axolotl/data/ConnectionStrings").GetAwaiter().GetResult();
                var secretResponse = JsonConvert.DeserializeObject<SecretResponse<ConnectionStringsConfig>>(secretJson);
                var cfg = secretResponse?.Data?.Data;
                connectionString = cfg!.Connection;
            }

            services.AddSingleton<string>(connectionString!);

            services.AddDbContext<ApplicationDbContext>((sp, options) =>
            {
                var resolvedConnection = sp.GetRequiredService<string>();
                options.UseNpgsql(resolvedConnection);
            });

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            // Repos
            services.AddScoped<IProductPriceHistoryRepository, ProductPriceHistoryRepository>();
            services.AddScoped<IProductStockRepository, ProductStockRepository>();
            services.AddScoped<ISalesRepository, SalesRepository>();
            services.AddScoped<IInvoicesRepository, InvoicesRepository>();
            services.AddScoped<IPaymentsRepository, PaymentsRepository>();
            services.AddScoped<IPaymentMethodsRepository, PaymentMethodsRepository>();

            // UoW
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Otros servicios
            services.AddTransient<IOrderingQuery, OrderingQuery>();
            services.AddTransient<IFileStorageService, FileStorageService>();

            // AutoMapper
            services.AddAutoMapper(typeof(ProductPriceHistoryProfile).Assembly);

            // MediatR
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(ChangePriceCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(Axolotl.Application.UseCases.Sales.Commands.CreateSale.CreateSaleCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(Axolotl.Application.UseCases.Invoices.Commands.CreateFromSale.CreateInvoiceFromSaleCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(Axolotl.Application.UseCases.Payments.Commands.CapturePayment.CapturePaymentCommand).Assembly);
            });

            // JWT
            services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));

            return services;
        }
    }
}
