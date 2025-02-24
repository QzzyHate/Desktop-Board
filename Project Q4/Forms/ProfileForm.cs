using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using Project_Q4.Models;
using Project_Q4.Services;

namespace Project_Q4.Forms
{
    public partial class ProfileForm : Form
    {
        private readonly UserRepository _userRepository;
        private readonly IServiceProvider _serviceProvider;
        private User _currentUser;

        public ProfileForm(UserRepository userRepository, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _userRepository = userRepository;
            _serviceProvider = serviceProvider;
        }

        public void SetCurrentUser(User user)
        {
            _currentUser = user;
            InitializeUI();
        }

        private void InitializeUI()
        {
            if (_currentUser == null)
            {
                MessageBox.Show("User data is not available.");
                return;
            }

            var nameLabel = new Label
            {
                Text = "Name:",
                Location = new Point(10, 10),
                AutoSize = true
            };
            this.Controls.Add(nameLabel);

            var nameTextBox = new TextBox
            {
                Text = _currentUser.UserName,
                Location = new Point(100, 10),
                ReadOnly = true,
                Width = 200
            };
            this.Controls.Add(nameTextBox);

            var emailLabel = new Label
            {
                Text = "Email:",
                Location = new Point(10, 40),
                AutoSize = true
            };
            this.Controls.Add(emailLabel);

            var emailTextBox = new TextBox
            {
                Text = _currentUser.UserEmail,
                Location = new Point(100, 40),
                ReadOnly = true,
                Width = 200
            };
            this.Controls.Add(emailTextBox);

            var roleLabel = new Label
            {
                Text = "Role:",
                Location = new Point(10, 70),
                AutoSize = true
            };
            this.Controls.Add(roleLabel);

            var roleTextBox = new TextBox
            {
                Text = _currentUser.Role.ToString(),
                Location = new Point(100, 70),
                ReadOnly = true,
                Width = 200
            };
            this.Controls.Add(roleTextBox);

            var subscriptionLabel = new Label
            {
                Text = "Subscription:",
                Location = new Point(10, 100),
                AutoSize = true
            };
            this.Controls.Add(subscriptionLabel);

            var subscriptionTextBox = new TextBox
            {
                Text = _currentUser.HasSubscription ? "Yes" : "No",
                Location = new Point(100, 100),
                ReadOnly = true,
                Width = 200
            };
            this.Controls.Add(subscriptionTextBox);

            //Кнопка Edit
            var editButton = new Button
            {
                Text = "Edit",
                Location = new Point(10, 130),
                Size = new Size(80, 30)
            };
            editButton.Click += (sender, e) =>
            {
                nameTextBox.ReadOnly = false;
                emailTextBox.ReadOnly = false;
            };
            this.Controls.Add(editButton);

            //Кнопка Save
            var saveButton = new Button
            {
                Text = "Save",
                Location = new Point(100, 130),
                Size = new Size(80, 30)
            };
            saveButton.Click += (sender, e) =>
            {
                try
                {
                    _userRepository.EditProfile(_currentUser.Id, nameTextBox.Text, emailTextBox.Text);
                    MessageBox.Show("Profile updated successfully!");
                    nameTextBox.ReadOnly = true;
                    emailTextBox.ReadOnly = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error updating profile: {ex.Message}");
                }
            };
            this.Controls.Add(saveButton);

            //Кнопка Become Leader
            if (_currentUser.Role != UserRole.Руководитель)
            {
                var becomeLeaderButton = new Button
                {
                    Text = "Get Leader Subscribe",
                    Location = new Point(190, 130),
                    Size = new Size(100, 30)
                };
                becomeLeaderButton.Click += (sender, e) =>
                {
                    try
                    {
                        _currentUser.Role = UserRole.Руководитель;
                        _userRepository.GrantSubscription(_currentUser.Id);
                        MessageBox.Show("You are now a Leader!");
                        roleTextBox.Text = _currentUser.Role.ToString();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error becoming a leader: {ex.Message}");
                    }
                };
                this.Controls.Add(becomeLeaderButton);
            }

            //Кнопка Back to Main
            var backButton = new Button
            {
                Text = "Back to Main",
                Location = new Point(10, this.ClientSize.Height - 50),
                Size = new Size(100, 30)
            };
            backButton.Click += (sender, e) =>
            {
                var mainForm = _serviceProvider.GetRequiredService<MainForm>();
                mainForm.SetCurrentUser(_currentUser);
                mainForm.Show();
                this.Close();
            };
            this.Controls.Add(backButton);
        }
    }
}