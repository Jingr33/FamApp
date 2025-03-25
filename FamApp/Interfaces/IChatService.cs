using FamApp.Models;
using FamApp.ViewModels;

namespace FamApp.Interfaces
{
    public interface IChatService
    {
        Task<Chat> GetChatByIdAsync(int chatId);
        Task<IEnumerable<Chat>> GetUserChatsAsync(string userId);
        Task SendMessageAsync(int chatId, string senderId, string message);
        Task AddChatAsync(CreateChatViewModel model, string thisUserId);
        Task<List<MessageViewModel>> GetMessagesForChat(int chatId);
    }
}
