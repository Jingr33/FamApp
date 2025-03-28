using AutoMapper;
using FamApp.Areas.Identity.Data;
using FamApp.Interfaces;
using FamApp.Models;
using FamApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public ChatsController(IChatService chatService, 
                               IUserService userService,
                               IMapper mapper,
                               UserManager<ApplicationUser> userManager)
        {
            _chatService = chatService;
            _userService = userService;
            //_mapper = mapper;
            //_userManager = userManager;
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
            var currentUser = await _userService.GetCurrentUserAsync();
            if (!ModelState.IsValid)
            {
                model.Users = await _chatService.GetUserSelectListAsync();
                model.CurrentUserId = await _userService.GetUserIdAsync(currentUser);
                return View(model);
            }

            await this._chatService.CreateChatAsync(model);
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
