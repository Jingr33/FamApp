using AutoMapper;
using FamApp.Areas.Identity.Data;
using FamApp.Interfaces;
using FamApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace FamApp.Controllers
{
    public class ChatsController : Controller
    {
        private readonly IChatService _chatService;
        private readonly IUserService _userService;

        public ChatsController(IChatService chatService, IUserService userService)
        {
            _chatService = chatService;
            _userService = userService;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var user = await _userService.GetCurrentUserAsync();
            var userId = await _userService.GetUserIdAsync(user);
            var chatViewModels = await _chatService.GetUserChatViewModelAsync(userId);
            return View(chatViewModels);
        }

        [HttpGet]
        public async Task<IActionResult> Chat(int chatId)
        {
            var chatViewModel = await _chatService.GetChatViewModelByIdAsync(chatId);
            if (chatViewModel == null)
                return NotFound();

            return PartialView("Chat", chatViewModel);
        }

        public async Task<IActionResult> CreateChat()
        {
            var createChatViewModel = await _chatService.GetCreateChatViewModel();
            return View(createChatViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateChat(CreateChatViewModel model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("CreateChat");

            Console.WriteLine($"ÚČASTNÍci CHATU: {model.SelectedUsers.Count}");
            await this._chatService.CreateChatAsync(model);
            return RedirectToAction("Index");
        }

        [HttpGet("GetMessages")]
        public async Task<IActionResult> GetMessages(int chatId)
        {
            var messageDtos = await _chatService.GetMessagesForChat(chatId);
            return Json(messageDtos);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteChat(int chatId)
        {
            await _chatService.DeleteChatAsync(chatId);
            return RedirectToAction("Index");
        }
    }
}
