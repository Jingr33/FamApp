using AutoMapper;
using FamApp.Areas.Identity.Data;
using FamApp.Interfaces;
using FamApp.Models;
using FamApp.ViewModels;

namespace FamApp.Services
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository _chatRepo;
        private readonly IMapper _mapper;

        public ChatService(IChatRepository chatRepo, IMapper mapper)
        {
            this._chatRepo = chatRepo;
            this._mapper = mapper;
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
                SentAt = DateTime.UtcNow,
                ChatId = chatId,
                SenderId = senderId,
            };
            await this._chatRepo.AddMessageAsync(message);
        }

        public async Task AddChatAsync (CreateChatViewModel model, string thisUserId)
        {
            model.SelectedUserIds.Add(thisUserId);
            Chat chat = this._mapper.Map<Chat>(model);
            chat.IsGroup = model.SelectedUserIds.Count > 1 ? true : false;
            await this._chatRepo.AddChatAsync(chat, model.SelectedUserIds);
        }

        public async Task<List<MessageViewModel>> GetMessagesForChat(int chatId)
        {
            return await _chatRepo.GetMessageForChatAsync(chatId);
        }
    }
}
