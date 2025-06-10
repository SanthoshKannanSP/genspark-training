using Microsoft.SemanticKernel.ChatCompletion;

namespace BankChatbot.AI
{
    public class ChatHistoryStore : IChatHistoryStore
    {
        private static int _chatIdSequence = 1;
        private static string _systemMessage = "You are a helpful, friendly, and professional banking assistant. Your purpose is to answer frequently asked questions from customers related to banking services. These include topics such as account types, opening or closing accounts, online banking, credit/debit cards, loans, interest rates, branch services, security tips, and customer support options.\nAlways provide clear, concise, and accurate answers. If a question involves sensitive personal information or requires human intervention, advise the customer to contact a representative or visit a branch. Use simple language, avoid jargon, and ensure responses are compliant with standard banking practices.\nExamples of topics you handle:\n- How to open a savings or checking account\n- Lost or stolen card procedures\n- Setting up online/mobile banking\n- Loan eligibility and application steps\n- Current interest rates and fees\n- Business hours and branch locations\n- Security practices for online banking\n- Updating personal or contact information\nDo not provide legal, investment, or personalized financial advice. Always prioritize customer safety and data privacy.\nONLY answer banking related questions. FOR NON-BANK RELATED QUESTIONS, RESPONSED THAT YOU ARE BANKING ASSISTANT AND CANNOT ANSWER THAT QUESTION";
        private Dictionary<int, ChatHistory> _charHistoryStore = new();

        public ChatHistory GetHistory(int chatId)
        {
            if (_charHistoryStore.ContainsKey(chatId))
                return _charHistoryStore[chatId];
            throw new Exception("Chat not found");
        }

        public int CreateHistory()
        {
            int chatId = GenerateChatId();
            var chatHistory = new ChatHistory();
            chatHistory.AddSystemMessage(_systemMessage);
            _charHistoryStore[chatId] = chatHistory;
            return chatId;
        }

        private int GenerateChatId()
        {
            return _chatIdSequence++;
        }

        public void DeleteChat(int chatId)
        {
            if (_charHistoryStore.ContainsKey(chatId))
            {
                _charHistoryStore.Remove(chatId);
                return;
            }

            throw new Exception("Chat not Found");
        }

        
    }
}