using FamApp.Areas.Identity.Data;
using FamApp.Data;
using FamApp.Interfaces;
using FamApp.Models;
using Microsoft.AspNetCore.Mvc;
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
            await this._db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Chat>> GetUserChatsAsync(string userId)
        {
            return await this._db.ChatUser
                .Where(uc => uc.UserId == userId)
                .Select(uc => uc.Chat)
                .ToListAsync();
        }

        public async Task<Chat> GetChatByIdAsync(int chatId)
        {
            return await this._db.Chat
                .Include(c => c.Messages)
                .FirstOrDefaultAsync(c => c.Id == chatId);
        }

        public async Task AddChatAsync(Chat chat, List<string> userIds)
        {
            this._db.Chat.Add(chat);
            await this._db.SaveChangesAsync();

            foreach (var userId in userIds)
            {
                this._db.ChatUser.Add(new ChatUser { ChatId = chat.Id, UserId = userId });
            }
            this._db.SaveChanges();
        }
    }
}
