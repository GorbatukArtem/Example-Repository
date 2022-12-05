using Datasource.Ef.Configurations.Content;
using Datasource.Ef.Domain.Content;
using Datasource.Ef.Seeds;
using Microsoft.EntityFrameworkCore;

namespace Datasource.Ef.Contexts
{
    public class DbContextPersonalCard : DbContext
    {
        public DbContextPersonalCard() { }
        public DbContextPersonalCard(DbContextOptions<DbContextPersonalCard> options) : base(options) { }

        public virtual DbSet<Person> Persons { get; set; }

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
            //#region Configurations
            modelBuilder.ApplyConfiguration(new ConfigurationPerson());
            //#endregion

            //#region Seeds
            modelBuilder.ApplyConfiguration(new SeedPerson());
            //#endregion
        }
    }
}
