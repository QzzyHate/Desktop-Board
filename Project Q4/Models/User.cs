using System;
using System.Collections.Generic;

namespace Project_Q4.Models
{
    public enum UserRole
    {
        Сотрудник,
        Руководитель
    }

    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string PasswordHash { get; set; }
        public UserRole Role { get; set; }
        public bool HasSubscription { get; set; }
        public ICollection<Board>? UserBoards { get; set; } = [];
        public ICollection<Task>? AssignedTasks { get; set; } = []; 
    }
}