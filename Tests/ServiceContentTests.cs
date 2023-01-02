using Tests.Persistence;

namespace Tests
{
    public class ServiceContentTests : TestsBase
    {
        [Fact]
        public async void GetAsync_When_Result_NotNull_Then_Success()
        {
            // arrange
            using var context = CreateDbContext();
            var repository = new RepositoryPerson(context);
            var service = new ServiceHome(repository);

            var seeds = new List<Person>() {
                new Person {
                    Id = 1,
                    LastName = "Романов",
                    FirstName = "Михаил",
                    MiddleName = "Федорович",
                    Birth = new DateTime(1596, 12, 07),
                    Death = new DateTime(1645, 07, 13),
                },
                new Person {
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
            var result = await service.GetAsync();

            // assert
            Assert.NotNull(result);
            Assert.Equal(2, result.PersonsTotal);
            Assert.Equal(0, result.AliveTotal);
            Assert.Equal(2, result.DeadTotal);
        }
    }
}
