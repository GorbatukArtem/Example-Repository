namespace Tests.Persistence;

public class TestsBase
{
    internal static DbContextPersonalCard CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<DbContextPersonalCard>()
        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
        .Options;

        var context = new DbContextPersonalCard(options);

        return context;
    }
}