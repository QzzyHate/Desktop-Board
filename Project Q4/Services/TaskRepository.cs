using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Project_Q4.Models;

namespace Project_Q4.Services
{
    public class TaskRepository
    {
        private readonly ApplicationDbContext _context;

        public TaskRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Models.Task CreateTask(int boardId, float x, float y)
        {
            var task = new Models.Task
            {
                Title = "New Task",
                Description = "",
                Priority = Models.TaskPriority.Низкий,
                Deadline = null,
                Reward = 0,
                Status = Models.TaskStatus.Свободна,
                PositionX = x,
                PositionY = y,
                BoardId = boardId
            };

            _context.Tasks.Add(task);
            _context.SaveChanges();
            return task;
        }

        public IEnumerable<Models.Task> GetTasksByBoardId(int boardId)
        {
            return _context.Tasks
                .Where(t => t.BoardId == boardId)
                .ToList();
        }

        public void UpdateTask(Models.Task updatedTask)
        {
            var task = _context.Tasks.Find(updatedTask.Id) ?? throw new KeyNotFoundException("Task not found(from UpdateTask)");

            task.Title = updatedTask.Title;
            task.Description = updatedTask.Description;
            task.Priority = updatedTask.Priority;
            task.Deadline = updatedTask.Deadline;
            task.Reward = updatedTask.Reward;
            task.Status = updatedTask.Status;
            task.PositionX = updatedTask.PositionX;
            task.PositionY = updatedTask.PositionY;

            _context.SaveChanges();
        }

        public void DeleteTask(int taskId)
        {
            var task = _context.Tasks.Find(taskId) ?? throw new KeyNotFoundException("Task not found(from DeleteTask)");

            _context.Tasks.Remove(task);
            _context.SaveChanges();
        }

        public void AssignTaskToUser(int taskId, int userId)
        {
            var task = _context.Tasks.Find(taskId) ?? throw new KeyNotFoundException("Task not found(from AssignTaskToUser)");

            var user = _context.Users.Find(userId) ?? throw new KeyNotFoundException("User not found(from AssignTaskToUser)");

            task.AssignedToId = userId;
            task.Status = Models.TaskStatus.Выполняется;
            _context.SaveChanges();
        }

        public void ChangeStatus(int taskId, Models.TaskStatus newStatus)
        {
            var task = _context.Tasks.Find(taskId) ?? throw new KeyNotFoundException("Task not found(from ChangeStatus)");

            task.Status = newStatus;
            _context.SaveChanges();
        }

        public Models.Task GetTaskWithAssignedUser(int taskId)
        {
            return _context.Tasks
                .Include(t => t.AssignedTo)
                .FirstOrDefault(t => t.Id == taskId);
        }
    }
}