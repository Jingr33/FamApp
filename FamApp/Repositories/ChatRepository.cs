using FamApp.Areas.Identity.Data;
using FamApp.Data;
using FamApp.Interfaces;
using FamApp.Models;
using FamApp.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FamApp.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private readonly ApplicationDbContext _db;

        public ChatRepository(ApplicationDbContext db)
        {
            this._db = db;
        }

        public async Task AddMessageAsync(Message message)
        {
            this._db.Message.Add(message);
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"[INNER] {ex.InnerException.Message}");
                }
                throw;
            }
        }

        public async Task<IEnumerable<Chat>> GetUserChatsAsync(string userId)
        {
            return await this._db.Chat
                .Where(c => c.UserChats.Any(uc => uc.UserId == userId))
                .Include(c => c.Messages)
                .Include(c => c.UserChats).ThenInclude(uc => uc.User)
                .ToListAsync();
        }

        public async Task<Chat> GetChatByIdAsync(int chatId)
        {
            return await this._db.Chat
                .Include(c => c.Messages)
                .FirstOrDefaultAsync(c => c.Id == chatId);
        }

        public async Task AddChatAsync(Chat chat, List<ApplicationUser> users)
        {
            this._db.Chat.Add(chat);
            await this._db.SaveChangesAsync();

            foreach (var user in users)
            {
                this._db.ChatUser.Add(new ChatUser { ChatId = chat.Id, UserId = user.Id });
            }
            this._db.SaveChanges();
        }

        public async Task<List<MessageViewModel>> GetMessageForChatAsync(int chatId)
        {
            return await _db.Message
            .Where(m => m.ChatId == chatId)
            .Include(m => m.Sender)
            .OrderBy(m => m.SentAt)
            .Select(m => new MessageViewModel
            {
                Content = m.Content,
                SenderId = m.SenderId,
                SenderNick = m.Sender.Nick,
                SentAt = m.SentAt.ToString("HH:mm dd.MM")
            })
            .ToListAsync();
        }

        public async Task DeleteChatAsync(Chat chat)
        {
            _db.Chat.Remove(chat);
            await this._db.SaveChangesAsync();
        }
    }
}
