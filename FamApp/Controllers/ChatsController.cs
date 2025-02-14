using FamApp.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FamApp.Controllers
{
    public class ChatsController : Controller
    {
        private readonly IChatService _chatService;

        public ChatsController(IChatService chatService)
        {
            this._chatService = chatService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new Exception("Uživatel nenalezen");
            var chats = await this._chatService.GetUserChatsAsync(userId);
            return View(chats);
        }

        public async Task<IActionResult> Chat(int chatId)
        {
            var chat = await this._chatService.GetChatByIdAsync(chatId);
            return View(chat);
        }
    }
}
