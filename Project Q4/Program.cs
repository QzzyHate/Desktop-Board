using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Project_Q4.Forms;
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
            ConfigureServices(services);

            var serviceProvider = services.BuildServiceProvider();

            var loginForm = serviceProvider.GetRequiredService<LogInForm>();
            Application.Run(loginForm);
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=TaskBoardDb;Trusted_Connection=True;")
                    .LogTo(Console.WriteLine, LogLevel.Information));

            services.AddScoped<UserRepository>();
            services.AddScoped<BoardRepository>();
            services.AddScoped<TaskRepository>();
            services.AddScoped<MessageRepository>();

            services.AddTransient<RegisterForm>();
            services.AddScoped<LogInForm>();
            services.AddScoped<MainForm>();
            services.AddScoped<BoardForm>();
            services.AddScoped<CreateBoardForm>();
            services.AddScoped<ProfileForm>();
        }
    }
}