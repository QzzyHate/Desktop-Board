using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Q4.Models
{
    public class Board
    {
        public int Id { get; set; }
        public string BoardName { get; set; }
        public int CreatorId { get; set; }
        public User Creator { get; set; }
        public ICollection<User> BoardUsers { get; set; } = [];
        public Chat Chat { get; set; }
        public ICollection<Task> Tasks { get; set; } = [];
    }
}