using FamApp.Models;
using FamApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FamApp.Interfaces
{
    public interface IChatRepository
    {
        Task<Chat> GetChatByIdAsync(int chatId);
        Task<IEnumerable<Chat>> GetUserChatsAsync(string userId);
        Task AddMessageAsync(Message message);
        Task AddChatAsync(Chat chat, List<string> userIds);
        Task<List<MessageViewModel>> GetMessageForChatAsync(int chatId);
    }
}
