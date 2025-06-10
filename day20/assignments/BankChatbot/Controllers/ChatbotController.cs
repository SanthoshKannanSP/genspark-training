using Microsoft.AspNetCore.Mvc;
using BankChatbot.Models.DTOs;
using BankChatbot.Services;

namespace BankChatbot.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ChatbotController
    {
        private readonly IChatbotService _chatbotService;
        public ChatbotController(IChatbotService chatbotService)
        {
            _chatbotService = chatbotService;
        }

        [HttpPost]
        public async Task<ActionResult<ChatResponseDTO>> Chat([FromBody] ChatRequestDTO chatRequestDTO)
        {
            return await _chatbotService.GenerateChatResponse(chatRequestDTO);
        }
    }
}