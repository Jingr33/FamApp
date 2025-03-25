using FamApp.Interfaces;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace FamApp.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IChatService _chatService;

        public ChatHub(IChatService chatService)
        {
            this._chatService = chatService;
        }

        public async Task JoinChat(int chatId)
        {
            Console.WriteLine($"[LOG] JoinChat - Přidávám {Context.ConnectionId} do skupiny {chatId}");
            await Groups.AddToGroupAsync(Context.ConnectionId, chatId.ToString());
        }

        public async Task SendMessage(int chatId, string message)
        {
            var user = Context.UserIdentifier;
            Console.WriteLine($"[LOG] SendMessage - chatId: {chatId}, user: {user}, message: {message}");
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new HubException("Zpráva nesmí být prázdná.");
            }
            await _chatService.SendMessageAsync(chatId, user, message);
            await Clients.Group(chatId.ToString()).SendAsync("ReceiveMessage", user, message);
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
