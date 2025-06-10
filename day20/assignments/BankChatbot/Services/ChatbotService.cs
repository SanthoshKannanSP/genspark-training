using System.Threading.Tasks;
using BankChatbot.AI;
using BankChatbot.Models.DTOs;
using Microsoft.SemanticKernel.ChatCompletion;

namespace BankChatbot.Services
{
    public class ChatbotService : IChatbotService
    {
        private readonly IChatbot _chatbot;
        private readonly IChatHistoryStore _historyStore;

        public ChatbotService(IChatbot chatbot, IChatHistoryStore historyStore)
        {
            _chatbot = chatbot;
            _historyStore = historyStore;
        }
        public void DeleteChat(int chatId)
        {
            throw new NotImplementedException();
        }

        public async Task<ChatResponseDTO> GenerateChatResponse(ChatRequestDTO chatRequestDTO)
        {
            if (chatRequestDTO.ChatId == null)
                chatRequestDTO.ChatId = _historyStore.CreateHistory();

            var history = _historyStore.GetHistory(chatRequestDTO.ChatId.Value);

            history.AddUserMessage(chatRequestDTO.UserInput);
            var chatOutput = await _chatbot.Chat(history);
            history.AddMessage(chatOutput.Role, chatOutput.Content ?? string.Empty);

            ChatResponseDTO response = new()
            {
                ChatId = chatRequestDTO.ChatId.Value,
                UserId = chatRequestDTO.UserId,
                ChatOutput = chatOutput.Content ?? string.Empty
            };
            return response;
        }
    }
}