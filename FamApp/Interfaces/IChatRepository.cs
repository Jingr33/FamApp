using FamApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace FamApp.Interfaces
{
    public interface IChatRepository
    {
        Task<Chat> GetChatByIdAsync(int chatId);
        Task<IEnumerable<Chat>> GetUserChatsAsync(string userId);
        Task AddMessageAsync(Message message);
    }
}
