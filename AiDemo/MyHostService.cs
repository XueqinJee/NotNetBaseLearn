using AiDemo.Services;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.VectorData;
using Microsoft.SemanticKernel.Connectors.Qdrant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace AiDemo
{
    internal class MyHostService : IHostedService
    {
        public readonly OllamaEmbeddingGenerator _generator;
        public readonly IVectorStoreRecordCollection<ulong, CloudService> _store;
        private readonly ILogger<MyHostService> _logger;

        public MyHostService(OllamaEmbeddingGenerator generator, IVectorStoreRecordCollection<ulong, CloudService> store
            , ILogger<MyHostService> logger)
        {
            _generator = generator;
            _store = store;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"开始更新：{DateTime.Now}");
            foreach (var item in cloudServices)
            {
                item.Vector = await _generator.GenerateEmbeddingVectorAsync(item.Description);
                await _store.UpsertAsync(item);
            }
            _logger.LogInformation($"更新完成：{DateTime.Now}");

            // Generate query embedding
            var query = "Which Azure service should I use to store my Word documents?";
            var queryEmbedding = await _generator.GenerateEmbeddingVectorAsync(query);
            // Query from vector data store
            var searchOptions = new VectorSearchOptions()
            {
                Top = 1, // Only return the Top 1 record from Qdrant
                VectorPropertyName = "Vector"
            };
            var results = await _store.VectorizedSearchAsync(queryEmbedding, searchOptions);
            await foreach (var result in results.Results)
            {
                Console.WriteLine($"Name: {result.Record.Name}");
                Console.WriteLine($"Description: {result.Record.Description}");
                Console.WriteLine($"Vector match score: {result.Score}");
                Console.WriteLine();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        List<CloudService> cloudServices = new List<CloudService>()
        {
            new CloudService
                {
                    Key=1,
                    Name="Azure App Service",
                    Description="Host .NET, Java, Node.js, and Python web applications and APIs in a fully managed Azure service. You only need to deploy your code to Azure. Azure takes care of all the infrastructure management like high availability, load balancing, and autoscaling."
                },
            new CloudService
                {
                    Key=2,
                    Name="Azure Service Bus",
                    Description="A fully managed enterprise message broker supporting both point to point and publish-subscribe integrations. It's ideal for building decoupled applications, queue-based load leveling, or facilitating communication between microservices."
                },
            new CloudService
                {
                    Key=3,
                    Name="Azure Blob Storage",
                    Description="Azure Blob Storage allows your applications to store and retrieve files in the cloud. Azure Storage is highly scalable to store massive amounts of data and data is stored redundantly to ensure high availability."
                },
            new CloudService
                {
                    Key=4,
                    Name="Microsoft Entra ID",
                    Description="Manage user identities and control access to your apps, data, and resources.."
                },
            new CloudService
                {
                    Key=5,
                    Name="Azure Key Vault",
                    Description="Store and access application secrets like connection strings and API keys in an encrypted vault with restricted access to make sure your secrets and your application aren't compromised."
                },
            new CloudService
                {
                    Key=6,
                    Name="Azure AI Search",
                    Description="Information retrieval at scale for traditional and conversational search applications, with security and options for AI enrichment and vectorization."
                }
        };
    }
}
