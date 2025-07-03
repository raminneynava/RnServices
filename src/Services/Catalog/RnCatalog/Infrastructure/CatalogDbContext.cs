using Microsoft.EntityFrameworkCore;

using RnCatalog.Models;

using System.Reflection;
namespace RnCatalog.Infrastructure;

public class CatalogDbContext(DbContextOptions<CatalogDbContext> dbContextOptions) : DbContext(dbContextOptions)
{
    private const string DefaultSchema = "catalog";
    public const string DefaultConnectionStringName = "DbContextString";

    public DbSet<CatalogBrand> CatalogBrands => Set<CatalogBrand>();
    public DbSet<CatalogItem> CatalogItems => Set<CatalogItem>();
    public DbSet<CatalogCategory> CatalogCategories => Set<CatalogCategory>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema(DefaultSchema);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
