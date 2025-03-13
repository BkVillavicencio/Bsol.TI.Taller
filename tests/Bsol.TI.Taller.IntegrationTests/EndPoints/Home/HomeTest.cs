using Bsol.TI.Taller.SharedKernel.Interfaces;
using Xunit;

namespace Bsol.TI.Taller.IntegrationTests.EndPoints.Home;

public class HomeTest : BaseEfRepositoryTest, IClassFixture<CustomWebApplicationFactory>
{
    private readonly string _url = "";
    private readonly CustomWebApplicationFactory _factory;
    public HomeTest(CustomWebApplicationFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task RetrieveHome()
    {
        var _httpClient = _factory.CreateClientWithMocks(services =>
        {
            var testRepository = GetTestRepository();
            testRepository.AddAsync(new("test", Core.Aggregates.Test.TestType.toDo));
            testRepository.SaveChangesAsync();
        });


        var response = await _httpClient.GetAsync(_url);

        Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
    }
}
