using ChatDemo;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;

var client = new HttpClient { BaseAddress = new Uri("http://172.16.15.188:4001/v1") };

var builder = Kernel.CreateBuilder();
builder.AddOpenAIChatCompletion("glm-4-flash", "sk-2FOYg9gmZ2oashWr1e2e9e467844489cB925494022C3FcE8", httpClient: client);
builder.Plugins.AddFromType<OrderPizzaPlugin>("OrderPizza");

var kernel = builder.Build();


ChatHistory chatHistory = [];
chatHistory.AddUserMessage("我想要订一个披萨，麻烦将菜单给我");

IChatCompletionService chatCompletion = kernel.GetRequiredService<IChatCompletionService>();

OpenAIPromptExecutionSettings openAIPromptExecutionSettings = new() {
    FunctionChoiceBehavior = FunctionChoiceBehavior.Auto(autoInvoke: false)
};

while (true) {
    var response = await chatCompletion.GetChatMessageContentAsync(chatHistory, executionSettings: openAIPromptExecutionSettings, kernel: kernel);
    Console.WriteLine(response.Content);

    if(response.Content is not null) {
        Console.WriteLine("\n退出~~~~~");
        return;
    }

    chatHistory.Add(response);

    IEnumerable<FunctionCallContent> functionCalls = FunctionCallContent.GetFunctionCalls(response);
    if (!functionCalls.Any()) {
        break;
    }

    foreach (FunctionCallContent functionCall in functionCalls) {
        try {
            FunctionResultContent resultContent = await functionCall.InvokeAsync(kernel);

            chatHistory.Add(resultContent.ToChatMessage());
        } catch (Exception ex) {
            chatHistory.Add(new FunctionResultContent(functionCall, ex).ToChatMessage());
        }
    }
}



