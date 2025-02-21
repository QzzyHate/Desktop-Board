using Microsoft.Extensions.DependencyInjection;
using Project_Q4.Forms;
using Project_Q4.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_Q4
{
    public partial class LogInForm : Form
    {
        private readonly UserRepository _userRepository;
        private readonly IServiceProvider _serviceProvider;

        public LogInForm(IServiceProvider serviceProvider, UserRepository userRepository)
        {
            InitializeComponent();
            _userRepository = userRepository;
            _serviceProvider = serviceProvider;
        }

        public void SetStartPosition(Point location)
        {
            this.StartPosition = FormStartPosition.Manual;
            this.Location = location;
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            try
            {
                string email = emailTextBox.Text.Trim();
                string password = passwordTextBox.Text;

                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    errorMessageLabel.Text = "Please enter email and password.";
                    return;
                }

                var user = _userRepository.Authorize(email, password);

                var currentPosition = this.Location;

                var mainForm = _serviceProvider.GetRequiredService<MainForm>();
                mainForm.SetStartPosition(currentPosition);
                mainForm.SetCurrentUser(user);
                mainForm.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                errorMessageLabel.Text = ex.Message;
                MessageBox.Show(ex.Message);
            }
        }

        private void createAccountButton_Click(object sender, EventArgs e)
        {
            var currentPosition = this.Location;

            var registerForm = _serviceProvider.GetRequiredService<RegisterForm>();
            registerForm.SetStartPosition(currentPosition);
            registerForm.Show();
            this.Hide();
        }
    }
}
