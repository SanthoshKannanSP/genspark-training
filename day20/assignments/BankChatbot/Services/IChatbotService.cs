using BankChatbot.Models.DTOs;
using Microsoft.Extensions.AI;

namespace BankChatbot.Services
{
    public interface IChatbotService
    {
        public Task<ChatResponseDTO> GenerateChatResponse(ChatRequestDTO chatRequestDTO);
        public void DeleteChat(int chatId);
    }
}