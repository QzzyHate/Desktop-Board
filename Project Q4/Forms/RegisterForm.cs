using Microsoft.Extensions.DependencyInjection;
using Project_Q4.Services;
using System.Windows.Forms;

namespace Project_Q4
{
    public partial class RegisterForm : Form
    {
        private readonly UserRepository _userRepository;
        private readonly IServiceProvider _serviceProvider;

        public RegisterForm(IServiceProvider serviceProvider, UserRepository userRepository)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _userRepository = userRepository;
        }

        public void SetStartPosition(Point location)
        {
            this.StartPosition = FormStartPosition.Manual;
            this.Location = location;
        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            try
            {
                string name = nameTextBox.Text;
                string email = emailTextBox.Text;
                string password = passwordTextBox.Text;

                _userRepository.Register(name, email, password);
                MessageBox.Show("Регистрация успешна!");

                var currentPosition = this.Location;

                var loginForm = _serviceProvider.GetRequiredService<LogInForm>();
                loginForm.SetStartPosition(currentPosition);
                loginForm.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                errorMessageLabel.Text = ex.Message;
            }
        }

        private void enterAccountButton_Click(object sender, EventArgs e)
        {
            var currentPosition = this.Location;

            var loginForm = _serviceProvider.GetRequiredService<LogInForm>();
            loginForm.SetStartPosition(currentPosition);
            loginForm.Show();
            this.Hide();
        }
    }
}