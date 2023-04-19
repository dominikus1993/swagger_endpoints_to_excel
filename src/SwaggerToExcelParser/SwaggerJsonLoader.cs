namespace SwaggerToExcelParser;

public sealed class SwaggerJsonLoader
{
    private readonly HttpClient _httpClient;

    public SwaggerJsonLoader(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> GetSwaggerSchema(string url, CancellationToken cancellationToken)
    {
        var resp = await _httpClient.GetStringAsync(url, cancellationToken);
        return resp;
    }
}
