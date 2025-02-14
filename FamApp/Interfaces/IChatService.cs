using FamApp.Models;

namespace FamApp.Interfaces
{
    public interface IChatService
    {
        Task<Chat> GetChatByIdAsync(int chatId);
        Task<IEnumerable<Chat>> GetUserChatsAsync(string userId);
        Task SendMessageAsync(int chatId, string senderId, string message);
    }
}
