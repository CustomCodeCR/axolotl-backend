using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Axolotl.Infrastructure.Persistence.Context;

namespace Axolotl.Infrastructure.Persistence.Context;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

        // 1) Try env var first (great for local/dev CI):
        var conn = Environment.GetEnvironmentVariable("DB_CONNECTION");

        // 2) Fallback (replace with your real local dev connection string):
        if (string.IsNullOrWhiteSpace(conn))
        {
            conn = "Host=localhost;Port=5432;Database=axolotl_db;Username=postgres;Password=postgres";
        }

        optionsBuilder.UseNpgsql(conn, npgsql =>
        {
            // Make sure migrations go to the assembly where your DbContext lives:
            npgsql.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
        });

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}