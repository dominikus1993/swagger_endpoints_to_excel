using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Cocona;
using SwaggerToExcelParser;

var builder = CoconaApp.CreateBuilder();
builder.Services.AddScoped<SwaggerJsonProcessor>();
builder.Services.AddHttpClient<SwaggerJsonLoader>();
builder.Services.AddScoped<SwaggerJsonProcessedDataWriter>();
var app = builder.Build();
app.AddCommand(async (string url,[FromService]SwaggerJsonLoader swaggerJsonLoader,[FromService]SwaggerJsonProcessor processor, [FromService]SwaggerJsonProcessedDataWriter writer, [FromService] ILogger<Program> logger,
            [FromService] CoconaAppContext context) =>
    {
            var resp = await swaggerJsonLoader.GetSwaggerSchema(url, context.CancellationToken);
            var endpoints = processor.ProcessSwaggerJson(resp);
            await writer.Save(endpoints, "endpoints.xlsx");
    })
    .WithDescription("Downloads deliveries points from Inpost API and then uploads them to solr.");

await app.RunAsync();