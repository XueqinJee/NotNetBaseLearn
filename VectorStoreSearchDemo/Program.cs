
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.SemanticKernel;
using VectorStoreSearchDemo;

var host = Host.CreateApplicationBuilder();

var kernelBuilder = host.Services.AddKernel();
kernelBuilder.AddOpenAIChatCompletion(
        "glm-4-flash",
        "sk-8s0tnQTLm6UDj93lC0D1A2D38d1c4aE8A33a22B95597F72a",
        "",
        httpClient: new HttpClient() { BaseAddress = new Uri("http://localhost:3000/v1")}
    );

host.Services.AddHostedService<RagChatService>();

var builder = host.Build();
await builder.RunAsync();