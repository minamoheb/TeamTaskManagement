using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using TeamTaskManagement.Services.Chat;
using TeamTaskManagement.Services.Dtos;

namespace TeamTaskManagement.API.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IChatService _chatService;
        private static readonly List<ChatMessage> _messageHistory = new List<ChatMessage>();
        public ChatHub(IChatService chatService)
        {
            _chatService = chatService;
        }

        public Task<List<ChatMessage>> GetMessageHistory()
        {
            return Task.FromResult(_messageHistory);
        }
        public async Task SendMessage([FromBody] ChatMessage message)
        {
            //var chatMessage = new ChatMessage
            //{
            //    User = user,
            //    Message = message
            //};

            _chatService.AddMessage(message);

            await Clients.All.SendAsync("ReceiveMessage", message);
        }


    }
}
