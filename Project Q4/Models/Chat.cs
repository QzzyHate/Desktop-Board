using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Q4.Models
{
    public class Chat
    {
        public int Id { get; set; }
        public ICollection<Message>? Messages { get; set; } = new List<Message>();
        public int BoardId { get; set; }
        public Board Board { get; set; }
    }
}