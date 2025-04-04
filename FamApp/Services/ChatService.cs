using AutoMapper;
using FamApp.Areas.Identity.Data;
using FamApp.Interfaces;
using FamApp.Models;
using FamApp.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FamApp.Services
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository _chatRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public ChatService(IChatRepository chatRepo,
                           IUserRepository userRepository,
                           IMapper mapper)
        {
            this._chatRepository = chatRepo;
            this._userRepository = userRepository;
            this._mapper = mapper;
        }

        public async Task<Chat> GetChatByIdAsync(int chatId)
        {
            return await this._chatRepository.GetChatByIdAsync(chatId);
        }

        public async Task<IEnumerable<Chat>> GetUserChatsAsync(string userId)
        {
            return await this._chatRepository.GetUserChatsAsync(userId);
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
            await this._chatRepository.AddMessageAsync(message);
        }

        public async Task AddChatAsync (CreateChatViewModel model, string thisUserId)
        {
            var creator = await _userRepository.GetUserByIdAsync(thisUserId);
            var selectedUsers = await _userRepository.GetUsersByIdsAsync(model.SelectedUsers);

            if (!selectedUsers.Any(u => u.Id == thisUserId))
            {
                selectedUsers.Add(creator);
            }

            Chat chat = this._mapper.Map<Chat>(model);
            chat.IsGroup = selectedUsers.Count >= 3 ? true : false;
            await this._chatRepository.AddChatAsync(chat, selectedUsers);
        }

        public async Task<List<MessageViewModel>> GetMessagesForChat(int chatId)
        {
            return await _chatRepository.GetMessageForChatAsync(chatId);
        }

        public async Task<List<ChatViewModel>> GetUserChatViewModelAsync(string userId)
        {
            var chats = await this.GetUserChatsAsync(userId);
            var chatViewModels = this._mapper.Map<List<ChatViewModel>>(chats);

            foreach (var (chatVM, chatEntity) in chatViewModels.Zip(chats, (vm, entity) => (vm, entity)))
            {
                chatVM.Participants = chatEntity.UserChats?.Where(cu => cu.User != null).Select(cu => cu.User.UserName).ToList();
                chatVM.Messages = chatEntity.Messages?.ToList() ?? new List<Message>();
            }

            return chatViewModels;
        }

        public async Task<ChatViewModel?> GetChatViewModelByIdAsync(int chatId)
        {
            var chat = await _chatRepository.GetChatByIdAsync(chatId);
            if (chat == null)
                return null;

            var user = await _userRepository.GetCurrentUserAsync();

            var chatViewModel = _mapper.Map<ChatViewModel>(chat);
            chatViewModel.CurrentUser = user.Id;

            return chatViewModel;
        }

        public async Task<CreateChatViewModel> GetCreateChatViewModel()
        {
            var currentUser = await _userRepository.GetCurrentUserAsync();
            var currentUserId = await _userRepository.GetUserIdAsync(currentUser);
            var users = await _userRepository.GetAllUsersAsync();

            var createChatViewModel = new CreateChatViewModel
            {
                CurrentUserId = currentUserId,
                Users = users.Select(u => new SelectListItem { Value = u.Id, Text = u.Nick }).ToList()
            };

            return createChatViewModel;
        }

        public async Task<List<SelectListItem>> GetUserSelectListAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return users.Select(u => new SelectListItem { Value = u.Id, Text = u.Nick }).ToList();
        }

        public async Task CreateChatAsync(CreateChatViewModel model)
        {
            var currentUser = await _userRepository.GetCurrentUserAsync();
            if (currentUser == null) 
                throw new Exception("Unknown currentUser.");

            string thisUserId = await _userRepository.GetUserIdAsync(currentUser);
            await AddChatAsync(model, thisUserId);
        }

        public async Task DeleteChatAsync(int chatId)
        {
            Chat chat = await _chatRepository.GetChatByIdAsync(chatId);
            if (chat == null)
                throw new Exception("Unknown chat.");

            await _chatRepository.DeleteChatAsync(chat);
        }
    }
}
