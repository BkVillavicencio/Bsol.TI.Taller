using Microsoft.AspNetCore.Mvc.Testing;

namespace Bsol.TI.Taller.IntegrationTests;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    private readonly WireMockSetup _wireMockSetup;
    public IConfiguration Configuration { get; private set; }
    public CustomWebApplicationFactory()
    {
        _wireMockSetup = new WireMockSetup();
    }


    public HttpClient CreateClientWithMocks(Action<IServiceCollection> configureMocks)
    {
        var client = WithWebHostBuilder(builder =>
        {
            builder.ConfigureTest(_wireMockSetup.BaseUrl);
            builder.ConfigureServices(services =>
            {
                configureMocks?.Invoke(services);
            });

        }).CreateClient();

        return client;
    }
}
public static class CustomAbstractionsWebHostBuilderExtensions
{
    public static IWebHostBuilder ConfigureTest(this IWebHostBuilder builder, string wireMockUrl)
    {
        builder.UseEnvironment("Test");
        builder.UseSetting("ExampleBaseURL", wireMockUrl);
        return builder;
    }

}
