using Microsoft.Data.SqlClient;

namespace Tests.Persistence;

public class TestsBase
{
    internal static DbContextPersonalCard CreateDbContext()
    {    
        var options = new DbContextOptionsBuilder<DbContextPersonalCard>()
        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
        .EnableDetailedErrors()
        .EnableSensitiveDataLogging()
        .UseChangeTrackingProxies(false)
        .UseLazyLoadingProxies(false)
        //.UseSqlServer(GetConnection())
        //.ConfigureWarnings(p => p.Ignore(InMemoryEventId.TransactionIgnoredWarning))
        .Options;

        var context = new DbContextPersonalCard(options);

        //context.Database.Migrate();

        context.Database.EnsureCreated();

        return context;
    }

    internal static SqlConnection GetConnection()
    {
        var connectionStringBuilder = new SqlConnectionStringBuilder { DataSource = ":memory:" };

        var connectionString = connectionStringBuilder.ToString();

        var connection = new SqlConnection(connectionString);
                
        connection.Open(); //The connection MUST be opened here

        return connection;
    }
}