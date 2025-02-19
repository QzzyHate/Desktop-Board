using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Q4.Models
{
    // Перечисление приоритетов задачи
    public enum TaskPriority
    {
        Низкий,
        Средний,
        Высокий
    }

    // Перечисление статусов задачи
    public enum TaskStatus
    {
        Свободна,
        Выполняется,
        Выполнена
    }

    public class Task
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskPriority Priority { get; set; }
        public DateTime? Deadline { get; set; }
        public float? Reward { get; set; }

        public float PositionX { get; set; }
        public float PositionY { get; set; }

        public TaskStatus Status { get; set; }
        public int? AssignedToId { get; set; }
        public User? AssignedTo { get; set; }
        public int BoardId { get; set; }
        public Board Board { get; set; }
    }
}