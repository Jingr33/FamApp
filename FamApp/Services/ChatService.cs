using FamApp.Interfaces;
using FamApp.Models;

namespace FamApp.Services
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository _chatRepo;

        public ChatService(IChatRepository chatRepo)
        {
            this._chatRepo = chatRepo;
        }

        public async Task<Chat> GetChatByIdAsync(int chatId)
        {
            return await this._chatRepo.GetChatByIdAsync(chatId);
        }

        public async Task<IEnumerable<Chat>> GetUserChatsAsync(string userId)
        {
            return await this._chatRepo.GetUserChatsAsync(userId);
        }

        public async Task SendMessageAsync(int chatId, string senderId, string content)
        {
            var message = new Message
            {
                Content = content,
                ChatId = chatId,
                SenderId = senderId,
                SentAt = DateTime.UtcNow,
            };
            await this._chatRepo.AddMessageAsync(message);
        }
    }
}
