using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

namespace BankChatbot.AI
{
    public interface IChatbot
    {
        public Task<ChatMessageContent> Chat(ChatHistory history);
    }
}