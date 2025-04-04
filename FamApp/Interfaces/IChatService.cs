using FamApp.Models;
using FamApp.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FamApp.Interfaces
{
    public interface IChatService
    {
        Task<Chat> GetChatByIdAsync(int chatId);
        Task<IEnumerable<Chat>> GetUserChatsAsync(string userId);
        Task SendMessageAsync(int chatId, string senderId, string message);
        Task AddChatAsync(CreateChatViewModel model, string thisUserId);
        Task<List<MessageViewModel>> GetMessagesForChat(int chatId);
        Task<List<ChatViewModel>> GetUserChatViewModelAsync(string userId);
        Task<ChatViewModel?> GetChatViewModelByIdAsync(int chatId);
        Task<CreateChatViewModel> GetCreateChatViewModel();
        Task CreateChatAsync(CreateChatViewModel model);
        Task<List<SelectListItem>> GetUserSelectListAsync();
        Task DeleteChatAsync(int chatId);
    }
}
