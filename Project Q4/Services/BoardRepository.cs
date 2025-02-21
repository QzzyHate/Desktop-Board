using Microsoft.EntityFrameworkCore;
using Project_Q4.Models;
using System.Collections.Generic;
using System.Linq;

namespace Project_Q4.Services
{
    public class BoardRepository
    {
        private readonly ApplicationDbContext _context;

        public BoardRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Board CreateBoard(string name, User creator)
        {
            EnsureUserIsLeader(creator);

            if (string.IsNullOrEmpty(name) || name.Length > 50)
                throw new ArgumentException("Board name must be between 1 and 50 characters.");

            var board = new Board
            {
                BoardName = name,
                Creator = creator,
                CreatorId = creator.Id,
                BoardUsers = new List<User> { creator },
                Chat = new Chat()
            };

            try { 
                _context.Boards.Add(board);
                _context.SaveChanges();
                return board;
            }
            catch(DbUpdateException ex)
            {
                throw new InvalidOperationException("Db error", ex);
            }
        }

        public Board GetBoard(int boardId)
        {
            var board = _context.Boards
                .Include(b => b.BoardUsers)
                .Include(b => b.Chat)
                .Include(b => b.Tasks)
                .FirstOrDefault(b => b.Id == boardId);

            if (board == null)
                throw new KeyNotFoundException($"Board with ID {boardId} not found.");

            return board;
        }

        public IEnumerable<Board> GetBoards(int userId)
        {
            return _context.Boards
                .Include(b => b.BoardUsers)
                .Where(b => b.CreatorId == userId || b.BoardUsers.Any(u => u.Id == userId))
                .ToList();
        }

        public void DeleteBoard(int boardId)
        {
            var board = _context.Boards.Find(boardId);
            if (board == null)
                throw new KeyNotFoundException($"Board with ID {boardId} not found.");

            try
            {
                _context.Boards.Remove(board);
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException("Failed to delete the board due to a database error.", ex);
            }
        }

        public void UpdateBoard(int boardId, string newName)
        {
            if (string.IsNullOrEmpty(newName))
                throw new ArgumentException("New board name cannot be null or empty.");

            var board = _context.Boards.Find(boardId);
            if (board == null)
                throw new KeyNotFoundException($"Board with ID {boardId} not found.");

            try
            {
                board.BoardName = newName;
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException("Failed to update the board due to a database error.", ex);
            }
        }

        public IEnumerable<User> GetBoardUsers(int boardId)
        {
            var board = _context.Boards
                .Include(b => b.BoardUsers)
                .FirstOrDefault(b => b.Id == boardId);

            if (board == null)
                throw new KeyNotFoundException($"Board with ID {boardId} not found.");

            return board.BoardUsers ?? new List<User>();
        }

        private void EnsureUserIsLeader(User user)
        {
            if (user.Role != UserRole.Руководитель)
            {
                throw new UnauthorizedAccessException("You do not have permission to perform this action.");
            }
        }
    }
}
