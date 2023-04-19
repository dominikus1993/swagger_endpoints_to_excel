using NJsonSchema;
using NSwag;

namespace SwaggerToExcelParser;

public sealed record Endpoint(string Method, string Path);

public sealed class SwaggerJsonProcessor
{  
    public async IAsyncEnumerable<Endpoint> ProcessSwaggerJson(string openapijson)
    {

        var openApiDocument = await OpenApiDocument.FromJsonAsync(openapijson);

        foreach (var path in openApiDocument.Paths)
        {
            foreach (var operation in path.Value)
            {
                yield return new Endpoint(operation.Key, path.Key);
            }
        }
        
    }
}
