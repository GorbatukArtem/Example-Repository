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
