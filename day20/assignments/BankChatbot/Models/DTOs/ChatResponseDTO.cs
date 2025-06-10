namespace BankChatbot.Models.DTOs
{
    public class ChatResponseDTO
    {
        public int UserId { get; set; }
        public int ChatId { get; set; }
        public string ChatOutput { get; set; } = string.Empty;
    }
}