using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Q4.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime SentAt { get; set; }
        public int SenderId { get; set; }
        public User Sender { get; set; }
        public int ChatId { get; set; }
        public Chat Chat { get; set; }
    }
}