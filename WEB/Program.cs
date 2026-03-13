using Blazor.Sonner.Extensions;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WEB;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://rsvp-api.nonstack.dev") });
builder.Services.AddScoped<ApiClient>();
builder.Services.AddSonner();

await builder.Build().RunAsync();