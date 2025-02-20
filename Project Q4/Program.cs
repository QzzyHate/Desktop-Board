using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Project_Q4.Services;
using System.Windows.Forms;

namespace Project_Q4
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            var services = new ServiceCollection();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=TaskBoardDb;Trusted_Connection=True;")
                       .LogTo(Console.WriteLine, LogLevel.Information));

            services.AddScoped<UserRepository>();
            services.AddScoped<BoardRepository>();
            services.AddScoped<TaskRepository>();

            services.AddTransient<Form1>();

            var serviceProvider = services.BuildServiceProvider();
            Application.Run(serviceProvider.GetRequiredService<Form1>());
        }
    }
}