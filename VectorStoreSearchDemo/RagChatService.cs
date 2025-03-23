using Microsoft.Extensions.Hosting;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.PromptTemplates.Handlebars;

namespace VectorStoreSearchDemo
{
    class RagChatService(
            Kernel kernel
        ) : IHostedService {
        public async Task StartAsync(CancellationToken cancellationToken) {

            while (!cancellationToken.IsCancellationRequested) {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("User：");
                var question = Console.ReadLine();

                var response = kernel.InvokePromptStreamingAsync(
                        promptTemplate: """
                            你只能说“中文”！    
                            Question: {{question}}
                        """,
                        arguments: new KernelArguments() {
                            {"question", question }
                        },
                        templateFormat: "handlebars",
                        promptTemplateFactory: new HandlebarsPromptTemplateFactory()
                    );
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("\nAssistant > ");

                try {
                    await foreach (var message in response.ConfigureAwait(false)) {
                        Console.Write(message);
                    }
                    Console.WriteLine();
                } catch (Exception ex) {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Call to LLM failed with error: {ex}");
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) {
            return Task.CompletedTask;
        }
    }
}
