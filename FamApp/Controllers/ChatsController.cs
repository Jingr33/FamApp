using AutoMapper;
using FamApp.Areas.Identity.Data;
using FamApp.Interfaces;
using FamApp.Models;
using FamApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FamApp.Controllers
{
    public class ChatsController : Controller
    {
        private readonly IChatService _chatService;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public ChatsController(IChatService chatService, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            this._chatService = chatService;
            this._mapper = mapper;
            this._userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new Exception("Uživatel nenalezen");
            var chats = await this._chatService.GetUserChatsAsync(userId);
            var chatViewModels = this._mapper.Map<List<ChatViewModel>>(chats);

            foreach (var (chatVM, chatEntity) in chatViewModels.Zip(chats, (vm, entity) => (vm, entity)))
            {
                chatVM.Participants = chatEntity.UserChats?.Where(cu => cu.User != null).Select(cu => cu.User.UserName).ToList();
                chatVM.Messages = chatEntity.Messages?.ToList() ?? new List<Message>();
            }

            return View(chatViewModels);
        }

        [HttpGet]
        public async Task<IActionResult> Chat(int chatId)
        {
            var chat = await this._chatService.GetChatByIdAsync(chatId);
            if (chat == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);

            var chatViewModel = this._mapper.Map<ChatViewModel>(chat);
            chatViewModel.CurrentUser = user.Id;
            return PartialView("Chat", chatViewModel);
        }

        public async Task<IActionResult> CreateChat()
        {
            ViewBag.Users = new SelectList(await this._userManager.Users.ToListAsync(), "Id", "Nick");
            ViewBag.ThisUserId = this._userManager.GetUserId(this.User);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateChat(CreateChatViewModel model)
        {
            string thisUserId = this._userManager.GetUserId(this.User) ?? throw new Exception("thisUserId is unknown");
            if (!ModelState.IsValid)
            {
                ViewBag.Users = new SelectList(await this._userManager.Users.ToListAsync(), "Id", "Nick");
                ViewBag.ThisUserId = thisUserId;
                return View(model);
            }

            await this._chatService.AddChatAsync(model, thisUserId);
            return RedirectToAction("Index");
        }

        [HttpGet("GetMessages")]
        public async Task<IActionResult> GetMessages(int chatId)
        {
            var messageDtos = await _chatService.GetMessagesForChat(chatId);
            return Json(messageDtos);
        }
    }
}
