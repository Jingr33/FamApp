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

        public async Task SendMessage(int chatId, string senderId, string message)
        {
            try
            {
                await this._chatService.SendMessageAsync(chatId, senderId, message);
                await Clients.Group(chatId.ToString()).SendAsync("RecieveMessage", senderId, message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("#############################################################");
                Console.WriteLine($"Chyba při odesílání zprávy: {ex.Message}");
                Console.WriteLine("#############################################################");
            }
        }

        public async Task JoinChat (string chatId)
        {
            try
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, chatId);
            }
            catch (Exception ex)
            {
                Console.WriteLine("#############################################################");
                Console.WriteLine($"Chyba při připojování do chatu: {ex.Message}");
                Console.WriteLine("#############################################################");
            }

        }

        public async Task LeaveChat (string chatId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatId);
        }
    }
}
