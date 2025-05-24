using System.Collections.Concurrent;
using TeamTaskManagement.Services.Dtos;

namespace TeamTaskManagement.Services.Chat
{
    public class ChatServices : IChatService
    {
        private readonly ConcurrentQueue<ChatMessage> _messages = new();

        public void AddMessage(ChatMessage message)
        {
            _messages.Enqueue(message);

            while (_messages.Count > 100)
            {
                _messages.TryDequeue(out _);
            }
        }

        public IEnumerable<ChatMessage> GetMessages(int count = 50)
        {
            return _messages.Reverse().Take(count).Reverse();
        }


    }
}
