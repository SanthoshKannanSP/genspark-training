using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
#pragma warning disable SKEXP0070 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.

namespace BankChatbot.AI
{
    public class Chatbot : IChatbot
    {
        string modelId = "devstral-small-2505";
        string apiKey = "";
        private IChatCompletionService _chatCompletionService;
        private Kernel _kernel;
        public Chatbot(IOptions<Secrets> secrets)
        {
            apiKey = secrets.Value.ApiKey;
            var builder = Kernel.CreateBuilder().AddMistralChatCompletion(modelId, apiKey: apiKey);
            _kernel = builder.Build();
            _chatCompletionService = _kernel.GetRequiredService<IChatCompletionService>();
        }

        public async Task<ChatMessageContent> Chat(ChatHistory history)
        {
            
                var result = await _chatCompletionService.GetChatMessageContentAsync(
                    history,kernel: _kernel);
                return result;
        }
    }
}






