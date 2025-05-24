using TeamTaskManagement.Services.Dtos;

namespace TeamTaskManagement.Services.Chat
{
    public interface IChatService
    {
        void AddMessage(ChatMessage message);
        IEnumerable<ChatMessage> GetMessages(int count = 50);
    }
}
