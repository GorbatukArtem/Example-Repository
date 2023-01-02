using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Ef.Datasource.Contexts;

public class DbContextPersonalCard : DbContext
{
    public DbContextPersonalCard() { }
    public DbContextPersonalCard(DbContextOptions<DbContextPersonalCard> options) : base(options) { }

    public new virtual DbSet<TEntity> Set<TEntity>() where TEntity : class
    {
        return base.Set<TEntity>();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseLazyLoadingProxies(false);
        options.UseChangeTrackingProxies(false);
        options.EnableSensitiveDataLogging(true);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        modelBuilder.HasDefaultSchema("dto");
        base.OnModelCreating(modelBuilder);
    }
}
