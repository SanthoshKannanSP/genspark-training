using Microsoft.SemanticKernel.ChatCompletion;

namespace BankChatbot.AI
{
    public interface IChatHistoryStore
    {
        public ChatHistory GetHistory(int chatId);
        public void DeleteChat(int chatId);

        public int CreateHistory();
    }
}