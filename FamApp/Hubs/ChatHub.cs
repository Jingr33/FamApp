using FamApp.Interfaces;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace FamApp.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IChatService _chatService;
        private readonly IUserService _userService;

        public ChatHub(IChatService chatService, IUserService userService)
        {
            this._chatService = chatService;
            _userService = userService;
        }

        public async Task JoinChat(int chatId)
        {
            Console.WriteLine($"[LOG] JoinChat - Přidávám {Context.ConnectionId} do skupiny {chatId}");
            await Groups.AddToGroupAsync(Context.ConnectionId, chatId.ToString());
        }

        public async Task SendMessage(int chatId, string message)
        {
            var userId = Context.UserIdentifier;
            var userNick = await _userService.GetUserNickByIdAsync(userId);
            Console.WriteLine($"[LOG] SendMessage - chatId: {chatId}, userId: {userId}, userNick: {userNick}, message: {message}");
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new HubException("Zpráva nesmí být prázdná.");
            }
            await _chatService.SendMessageAsync(chatId, userId, message);
            await Clients.Group(chatId.ToString()).SendAsync("ReceiveMessage", userNick, message);
        }

        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();

            if (httpContext.Request.Query.TryGetValue("chatId", out var chatIdStr) && int.TryParse(chatIdStr, out int chatId))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, chatId.ToString());
                Console.WriteLine($"Uživatel {Context.ConnectionId} se připojil ke skupině {chatId}");
            }

            await base.OnConnectedAsync();
        }
    }
}
