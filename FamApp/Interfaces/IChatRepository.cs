using FamApp.Areas.Identity.Data;
using FamApp.Models;
using FamApp.ViewModels;

namespace FamApp.Interfaces
{
    public interface IChatRepository
    {
        Task<Chat> GetChatByIdAsync(int chatId);
        Task<IEnumerable<Chat>> GetUserChatsAsync(string userId);
        Task AddMessageAsync(Message message);
        Task AddChatAsync(Chat chat, List<ApplicationUser> users);
        Task<List<MessageViewModel>> GetMessageForChatAsync(int chatId);
        Task DeleteChatAsync(Chat chat);
    }
}
