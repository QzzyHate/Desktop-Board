using Microsoft.EntityFrameworkCore;
using Project_Q4.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public ApplicationDbContext()
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Board> Boards { get; set; }
    public DbSet<Project_Q4.Models.Task> Tasks { get; set; }
    public DbSet<Chat> Chats { get; set; }
    public DbSet<Project_Q4.Models.Message> Messages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Board>()
            .HasOne(b => b.Creator)
            .WithMany()
            .HasForeignKey(b => b.CreatorId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Board>()
            .HasMany(b => b.BoardUsers)
            .WithMany(u => u.UserBoards)
            .UsingEntity(j => j.ToTable("BoardUsers"));

        modelBuilder.Entity<Project_Q4.Models.Task>()
            .HasOne(t => t.AssignedTo)
            .WithMany(u => u.AssignedTasks)
            .HasForeignKey(t => t.AssignedToId);

        modelBuilder.Entity<Project_Q4.Models.Message>()
            .HasOne(m => m.Chat)
            .WithMany(c => c.Messages)
            .HasForeignKey(m => m.ChatId);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(
                @"Server=(localdb)\mssqllocaldb;Database=TaskBoardDb;Trusted_Connection=True;");
        }
    }
}