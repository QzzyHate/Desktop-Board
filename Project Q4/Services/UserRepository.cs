using Project_Q4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Project_Q4.Services
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(ApplicationDbContext context) : base(context) { }

        public void Register(string name, string email, string password)
        {
            if (_context.Users.Any(u => u.UserEmail == email))
                throw new Exception($"User with {email} already exists.");
            
            var user = new User
            {
                UserName = name,
                UserEmail = email,
                PasswordHash = HashPassword(password),
                Role = UserRole.Сотрудник,
                HasSubscription = false
            };
            
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public User Authorize(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserEmail == email) ?? throw new Exception($"User with {email} not found");

            if (!VerifyPassword(password, user.PasswordHash))
                throw new Exception("Incorrect email or password");

            return user;
        }

        public User GetProfile(int userId)
        {
            var user = GetById(userId);

            return user ?? throw new Exception($"User with {userId} not found");
        }

        public bool HasSubscription(int userId)
        {
            var user = GetById(userId);
            return user?.HasSubscription ?? false;
        }

        public void EditProfile(int userId, string? newName, string? newEmail)
        {
            var user = GetById(userId) ?? throw new Exception($"User with {userId} not found");

            user.UserName = string.IsNullOrEmpty(newName) ? user.UserName : newName;
            user.UserEmail = string.IsNullOrEmpty(newEmail) ? user.UserEmail : newEmail;

            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void GrantSubscription(int userId)
        {
            var user = GetById(userId) ?? throw new Exception($"User with {userId} not found");

            user.HasSubscription = true;
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public IEnumerable<Board> GetBoards(int userId)
        {
            return [.. _context.Boards.Where(b => b.CreatorId == userId)];
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        private bool VerifyPassword(string password, string hash)
        {
            return HashPassword(password) == hash;
        }
    }
}