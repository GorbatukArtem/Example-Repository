using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Ef.Datasource.Contexts
{
    public class DbContextPersonalCard : DbContext
    {
        public DbContextPersonalCard() { }
        public DbContextPersonalCard(DbContextOptions<DbContextPersonalCard> options) : base(options) { }

        public new DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseLazyLoadingProxies(false);

            // во время запуска тестов без этой проверки возникает ошибка 'Multiple database providers in service provider'  .
            // InMemory создает свою конфигурацию.
            if (!options.IsConfigured)
            {
                options.UseSqlServer("connection_string");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var entityTypeConfigurations = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(type => !string.IsNullOrEmpty(type.Namespace))
                .Where(type => type.BaseType != null && 
                    type.BaseType.IsGenericType && 
                    type.BaseType.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>));
            
            foreach (var configuration in entityTypeConfigurations)
            {
                dynamic? instance = Activator.CreateInstance(configuration);

                if (instance != null)
                {
                    modelBuilder.ApplyConfiguration(instance);
                }
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}
