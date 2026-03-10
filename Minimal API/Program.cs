using Microsoft.SemanticKernel;
using Minimal_API;
using System.Threading.RateLimiting;

#region Configuration

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

    options.GlobalLimiter = PartitionedRateLimiter.CreateChained(
        // Client-based limiter: 3 requests per hour per client ID
        PartitionedRateLimiter.Create<HttpContext, string>(context =>
        {
            var clientId = context.Request.Headers["X-Client-Id"].ToString() ?? "anonymous";
            return RateLimitPartition.GetFixedWindowLimiter(
                partitionKey: $"id_{clientId}",
                factory: _ => new FixedWindowRateLimiterOptions
                {
                    PermitLimit = 3,
                    Window = TimeSpan.FromHours(1),
                    AutoReplenishment = true
                });
        }),
        // IP-based limiter: 9 requests per hour per IP address
        PartitionedRateLimiter.Create<HttpContext, string>(context =>
        {
            var ip = context.Connection.RemoteIpAddress?.ToString() ?? "unknown_ip";
            return RateLimitPartition.GetFixedWindowLimiter(
                partitionKey: $"ip_{ip}",
                factory: _ => new FixedWindowRateLimiterOptions
                {
                    PermitLimit = 9,
                    Window = TimeSpan.FromHours(1),
                    AutoReplenishment = true
                });
        })
    );
});

builder.Services.AddTransient(sp =>
{
    var kernelBuilder = Kernel.CreateBuilder();
    kernelBuilder.AddOpenAIChatCompletion(
        modelId: "gpt-5-nano",
        apiKey: builder.Configuration["AI:ApiKey"] ?? throw new InvalidOperationException("AI API key is not configured.")
    );

    var kernel = kernelBuilder.Build();
    var pluginsPath = Path.Combine(AppContext.BaseDirectory, "Plugins", "RsvpPlugin");
    kernel.ImportPluginFromPromptDirectory(pluginsPath);
    return kernel;
});

var app = builder.Build();
app.UseCors();
app.UseRateLimiter();
app.UseForwardedHeaders();

#endregion


app.MapGet("/", () => "API is running!");

app.MapGet("/GetAnInterestingText", async (string language, string topic, Kernel kernel) =>
    StreamText(language, topic, kernel));

static async IAsyncEnumerable<string> StreamText(string language, string topic, Kernel kernel)
{
    var anchors = Anchors.Pick(3);
    Console.WriteLine($"Selected anchors: {anchors}");

    var nicheResult = await kernel.InvokeAsync("RsvpPlugin", "PickANiche", new() { ["topic"] = topic, ["anchors"] = anchors });
    string nicheOutput = nicheResult.ToString();

    if (nicheOutput.Contains("Fact:"))
        nicheOutput = nicheOutput.Split("Fact:").Last().Trim();

    Console.WriteLine($"Selected niche: {nicheOutput}");

    await foreach (var chunk in kernel.InvokeStreamingAsync<string>("RsvpPlugin", "GenerateText", new() { ["niche_topic"] = nicheOutput, ["language"] = language }))
    {
        if (!string.IsNullOrEmpty(chunk))
        {
            Console.Write(chunk);
            yield return chunk;
        }
    }

    Console.WriteLine("\n--- End of stream ---");
}

app.Run();