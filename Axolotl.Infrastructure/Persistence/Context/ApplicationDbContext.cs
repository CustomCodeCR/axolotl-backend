using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Reflection;
using Axolotl.Domain.Entities;

namespace Axolotl.Infrastructure.Persistence.Context;

public partial class ApplicationDbContext : DbContext

{
    private readonly string _connectionString;

    public DbSet<Products> Products => Set<Products>();

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, string connectionString)
        : base(options)
    {
        _connectionString = connectionString;
    }

    public NpgsqlConnection CreateConnection => new NpgsqlConnection(_connectionString);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}