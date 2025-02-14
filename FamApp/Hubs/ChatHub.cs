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
            await this._chatService.SendMessageAsync(chatId, senderId, message);
            await Clients.Group(chatId.ToString()).SendAsync("RecieveMessage", senderId, message);
        }
    }
}
