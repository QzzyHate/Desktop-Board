using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Q4.Services
{
    public class MessageRepository
    {
        private readonly ApplicationDbContext _context;

        public MessageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Models.Message SendMessage(int chatId, int senderId, string text)
        {
            var chat = _context.Chats.Find(chatId);
            if (chat == null)
                throw new KeyNotFoundException("Chat not found(from SendMessage");

            var sender = _context.Users.Find(senderId);
            if (sender == null)
                throw new KeyNotFoundException("Sender not found(from SendMessage");

            var message = new Models.Message
            {
                Text = text,
                SentAt = DateTime.Now,
                SenderId = senderId,
                ChatId = chatId
            };

            _context.Messages.Add(message);
            _context.SaveChanges();
            return message;
        }

        public IEnumerable<Models.Message> GetMessagesByChatId(int chatId)
        {
            return _context.Messages
                .Where(m => m.ChatId == chatId)
                .OrderBy(m => m.SentAt)
                .ToList();
        }

        public void DeleteMessage(int messageId)
        {
            var message = _context.Messages.Find(messageId);
            if (message == null)
                throw new KeyNotFoundException("Message not found(from DeleteMessage)");

            _context.Messages.Remove(message);
            _context.SaveChanges();
        }
    }
}
