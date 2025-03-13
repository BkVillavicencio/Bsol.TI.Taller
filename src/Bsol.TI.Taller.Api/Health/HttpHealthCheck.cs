using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Bsol.TI.Taller.Api.Health;

public class HttpHealthCheck : IHealthCheck
{
    private readonly string? _baseUrl;
    private readonly HttpClient _httpClient;
    public HttpHealthCheck(string? baseUrl)
    {
        _baseUrl = baseUrl;
        _httpClient = new HttpClient();
    }

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/health", cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                return HealthCheckResult.Healthy();
            }
            else
            {
                return HealthCheckResult.Unhealthy();
            }
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy(ex.Message);
        }
    }
}
