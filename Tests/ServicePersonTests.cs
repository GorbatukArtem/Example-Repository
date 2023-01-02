using Tests.Persistence;

namespace Tests;

public class ServicePersonTests : TestsBase
{
    [Fact]
    public async void Create_When_Result_NotNull_Then_Success()
    {
        // arrange
        using var context = CreateDbContext();
        var repository = new RepositoryPerson(context);
        var service = new ServicePerson(repository);

        var createModel = new PersonCreate
        {
            LastName = "Романов",
            FirstName = "Михаил",
            MiddleName = "Федорович",
            Birth = new DateTime(1596, 12, 07),
            Death = new DateTime(1645, 07, 13),
        };

        // act
        await service.Create(createModel);

        // assert
        var results = context.Set<Person>().AsNoTracking().ToList();
        Assert.NotNull(results);
        var result = Assert.Single(results);
        Assert.Equal("Романов", result.LastName);
        Assert.Equal("Михаил", result.FirstName);
        Assert.Equal("Федорович", result.MiddleName);
        Assert.Equal(new DateTime(1596, 12, 07), result.Birth);
        Assert.Equal(new DateTime(1645, 07, 13), result.Death);
    }

    [Fact]
    public async void Update_When_Result_NotNull_Then_Success()
    {
        // arrange
        using var context = CreateDbContext();
        var repository = new RepositoryPerson(context);
        var service = new ServicePerson(repository);

        var seed = new Person
        {
            Id = 1,
            LastName = "Романов Михаил",
            FirstName = string.Empty,
            MiddleName = "Федорович",
            Birth = new DateTime(1596, 12, 07),
            Death = new DateTime(1645, 07, 13),
        };

        await context.Set<Person>().AddAsync(seed);
        await context.SaveChangesAsync();

        var updateModel = new PersonUpdate
        {
            Id = 1,
            LastName = "Романов",
            FirstName = "Михаил",
            MiddleName = "Федорович",
            Birth = new DateTime(1596, 12, 07),
            Death = new DateTime(1645, 07, 13),
        };

        // act
        await service.Update(updateModel);

        // assert
        var results = context.Set<Person>().AsNoTracking().ToList();
        Assert.NotNull(results);
        var result = Assert.Single(results);
        Assert.Equal("Романов", result.LastName);
        Assert.Equal("Михаил", result.FirstName);
        Assert.Equal("Федорович", result.MiddleName);
        Assert.Equal(new DateTime(1596, 12, 07), result.Birth);
        Assert.Equal(new DateTime(1645, 07, 13), result.Death);
    }

    [Fact]
    public async void Delete_When_Result_Empty_Then_Success() 
    {
        // arrange
        using var context = CreateDbContext();
        var repository = new RepositoryPerson(context);
        var service = new ServicePerson(repository);

        var seed = new Person
        {
            Id = 1,
            LastName = "Романов Михаил",
            FirstName = string.Empty,
            MiddleName = "Федорович",
            Birth = new DateTime(1596, 12, 07),
            Death = new DateTime(1645, 07, 13),
        };

        await context.Set<Person>().AddAsync(seed);
        await context.SaveChangesAsync();

        // act
        await service.Delete(1);

        // assert
        var results = context.Set<Person>().AsNoTracking().ToList();
        Assert.NotNull(results);
        Assert.Empty(results);
    }

    [Fact]
    public async void Get_When_Result_NotNull_Then_Success() 
    {
        // arrange
        using var context = CreateDbContext();
        var repository = new RepositoryPerson(context);
        var service = new ServicePerson(repository);

        var seeds = new List<Person>() {
            new Person
            {
                Id = 1,
                LastName = "Романов",
                FirstName = "Михаил",
                MiddleName = "Федорович",
                Birth = new DateTime(1596, 12, 07),
                Death = new DateTime(1645, 07, 13),
            },
            new Person
            {
                Id = 2,
                LastName = "Романов",
                FirstName = "Алексей",
                MiddleName = "Михайлович",
                Birth = new DateTime(1629, 03, 19),
                Death = new DateTime(1676, 02, 08),
            } 
        };

        await context.Set<Person>().AddRangeAsync(seeds);
        await context.SaveChangesAsync();

        // act
        var results = await service.GetAll();

        // assert
        Assert.NotNull(results);
        Assert.Equal(2, results.Personals.Count());
    }
}
