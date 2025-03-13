using WireMock.Server;

namespace Bsol.TI.Taller.IntegrationTests;

public class WireMockSetup : IDisposable
{
    private readonly WireMockServer _server;
    private readonly List<Action<WireMockServer>> _configurations;

    public WireMockSetup()
    {
        _server = WireMockServer.Start();
        _configurations = new List<Action<WireMockServer>>();

        Console.WriteLine($"WireMock server started at {_server.Urls[0]}");
    }
    public void AddConfiguration(Action<WireMockServer> configure)
    {
        _configurations.Add(configure);
    }

    public void ApplyConfigurations()
    {
        foreach (var configure in _configurations)
        {
            configure(_server);
        }
    }

    public string BaseUrl => _server.Urls[0];

    public void Dispose()
    {
        _server.Stop();
        _server.Dispose();
    }
}
