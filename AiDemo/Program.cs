using AiDemo;
using AiDemo.Services;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.SemanticKernel.Connectors.Qdrant;
using Qdrant.Client;

var host = Host.CreateDefaultBuilder()
    .ConfigureHostConfiguration(options => {
        options.AddJsonFile($"appsettings.json", optional: false, true);
    })
    .ConfigureServices((context, services) => {
        // 加载 ollama 配置
        var config = context.Configuration;
        var ollama = new OllamaEmbeddingGenerator(new Uri(config["Embedding:EndPoint"].ToString()), 
                config["Embedding:Model"]);

        services.AddSingleton(ollama);

        // 向量库
        var vectorStore = new QdrantVectorStore(new QdrantClient(config["Qdrant:Host"], int.Parse(config["Qdrant:Port"])));
        var cloudServicesStore = vectorStore.GetCollection<ulong, CloudService>("cloudServices");
        cloudServicesStore.CreateCollectionIfNotExistsAsync().Wait();

        services.AddSingleton(cloudServicesStore);
        services.AddHostedService<MyHostService>();
    })
    .Build();

host.Run();