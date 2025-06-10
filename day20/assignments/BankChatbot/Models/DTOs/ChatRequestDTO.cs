namespace BankChatbot.Models.DTOs
{
    public class ChatRequestDTO
    {
        public int UserId { get; set; }
        public int? ChatId { get; set; } = null;
        public string UserInput { get; set; } = string.Empty;
    }
}