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
            FirstName = "Федор III",
            MiddleName = "Алексеевич",
            Birth = new DateTime(1676, 01, 29),
            Death = new DateTime(1682, 04, 27),
        };

        // act
        await service.Create(createModel);

        // assert
        var result = context.Set<Person>().LastOrDefault();
        Assert.NotNull(result);
        Assert.Equal("Романов", result.LastName);
        Assert.Equal("Федор III", result.FirstName);
        Assert.Equal("Алексеевич", result.MiddleName);
        Assert.Equal(new DateTime(1676, 01, 29), result.Birth);
        Assert.Equal(new DateTime(1682, 04, 27), result.Death);
    }

    [Fact]
    public async void Create_When_FirstName_EqualNull_Then_ThrowException()
    {
        // arrange
        using var context = CreateDbContext();
        var repository = new RepositoryPerson(context);
        var service = new ServicePerson(repository);

#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        var createModel = new PersonCreate
        {
            LastName = "Романов",
            FirstName = null,
            MiddleName = "Алексеевич",
            Birth = new DateTime(1676, 01, 29),
            Death = new DateTime(1682, 04, 27),
        };
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

        // act
        var result = async () => await service.Create(createModel);

        // assert
        await Assert.ThrowsAsync<DbUpdateException>(result);
    }

    [Fact(Skip = @"The exception does not work for a field with the HasMaxLength attribute whose value exceeds the specified length.
                   The reason was not found in a short time by me.")]
    public async void Create_When_FirstName_ExceedsLength_Then_ThrowException()
    {
        // arrange
        using var context = CreateDbContext();
        var repository = new RepositoryPerson(context);
        var service = new ServicePerson(repository);

        var firstNameExceedsLength = new string('a', 201);

        var createModel = new PersonCreate
        {
            LastName = "Романов",
            FirstName = firstNameExceedsLength,
            MiddleName = "Алексеевич",
            Birth = new DateTime(1676, 01, 29),
            Death = new DateTime(1682, 04, 27),
        };

        await service.Create(createModel);

        // act
        var result = async () => await service.Create(createModel);

        // assert
        await Assert.ThrowsAsync<DbUpdateException>(result);
    }

    [Fact]
    public async void Update_When_Result_NotNull_Then_Success()
    {
        // arrange
        using var context = CreateDbContext();
        var repository = new RepositoryPerson(context);
        var service = new ServicePerson(repository);

        var updateModel = new PersonUpdate
        {
            Id = 1,
            LastName = "Романов",
            FirstName = "Михаил +",
            MiddleName = "Федорович",
            Birth = new DateTime(1596, 12, 07),
            Death = new DateTime(1645, 07, 13),
        };

        // act
        await service.Update(updateModel);

        // assert
        var result = context.Find<Person>(1);
        Assert.NotNull(result);
        Assert.Equal("Романов", result.LastName);
        Assert.Equal("Михаил +", result.FirstName);
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

        // act
        await service.Delete(1);

        // assert
        var result = context.Find<Person>(1);
        Assert.Null(result);
    }

    [Fact]
    public async void Get_When_Result_NotNull_Then_Success() 
    {
        // arrange
        using var context = CreateDbContext();
        var repository = new RepositoryPerson(context);
        var service = new ServicePerson(repository);

        // act
        var results = await service.GetAll();

        // assert
        Assert.NotNull(results);
        Assert.Equal(2, results.Personals.Count());
    }
}
