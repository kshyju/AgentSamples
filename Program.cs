using AgentSamples;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;

var endpoint = Environment.GetEnvironmentVariable("AZURE_OPENAI_ENDPOINT");
var model = Environment.GetEnvironmentVariable("AZURE_OPENAI_MODEL");
var apiKey = Environment.GetEnvironmentVariable("AZURE_OPENAI_APIKEY");

if (string.IsNullOrEmpty(endpoint) || string.IsNullOrEmpty(model) || string.IsNullOrEmpty(apiKey))
{
    Console.WriteLine("Please set the AZURE_OPENAI_ENDPOINT, AZURE_OPENAI_MODEL, and AZURE_OPENAI_APIKEY environment variables.");
    return;
}

var builder = Kernel.CreateBuilder();

builder.AddAzureOpenAIChatCompletion(model, endpoint, apiKey);

builder.Plugins.AddFromType<MyNewsPlugin>();

var kernel = builder.Build();

var chatService = kernel.GetRequiredService<IChatCompletionService>();

var executionSettings = new OpenAIPromptExecutionSettings { ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions };
ChatHistory chatHistory = new();
while (true)
{
    Console.Write("Prompt:");
    var userInput = Console.ReadLine();  

    chatHistory.AddUserMessage(userInput);


    var response = chatService.GetStreamingChatMessageContentsAsync(chatHistory, executionSettings, kernel: kernel);

    var message = string.Empty;

    await foreach (var content in response)
    {
        Console.Write(content);
        message += content;
    }
    chatHistory.AddAssistantMessage(message);
    Console.WriteLine();
}