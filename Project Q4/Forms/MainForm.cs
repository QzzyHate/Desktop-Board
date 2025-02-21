using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using Project_Q4.Models;
using Project_Q4.Services;

namespace Project_Q4.Forms
{
    public partial class MainForm : Form
    {
        private readonly BoardRepository _boardRepository;
        private readonly IServiceProvider _serviceProvider;
        private User _currentUser;

        public MainForm(BoardRepository boardRepository, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _boardRepository = boardRepository;
            _serviceProvider = serviceProvider;
        }

        public void SetStartPosition(Point location)
        {
            this.StartPosition = FormStartPosition.Manual;
            this.Location = location;
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

            var welcomeLabel = new Label
            {
                Text = $"Welcome, {_currentUser.UserName}",
                AutoSize = true,
                Location = new Point(10, 10),
                Font = new Font("Arial", 12, FontStyle.Bold)
            };
            this.Controls.Add(welcomeLabel);

            var profileButton = new Button
            {
                Text = "Profile",
                Location = new Point(this.ClientSize.Width - 200, 10),
                Size = new Size(80, 30)
            };
            profileButton.Click += ProfileButton_Click;
            this.Controls.Add(profileButton);

            var logoutButton = new Button
            {
                Text = "Logout",
                Location = new Point(this.ClientSize.Width - 100, 10),
                Size = new Size(80, 30)
            };
            logoutButton.Click += LogoutButton_Click;
            this.Controls.Add(logoutButton);

            var boardsPanel = new FlowLayoutPanel
            {
                Location = new Point(10, 50),
                Size = new Size(this.ClientSize.Width - 20, this.ClientSize.Height - 150),
                AutoScroll = true,
                FlowDirection = FlowDirection.TopDown
            };
            LoadBoards(boardsPanel);
            this.Controls.Add(boardsPanel);

            if (_currentUser.Role == UserRole.Руководитель)
            {
                var createBoardButton = new Button
                {
                    Text = "+ Create New Board",
                    Location = new Point(10, this.ClientSize.Height - 50),
                    Size = new Size(150, 30)
                };
                createBoardButton.Click += CreateBoardButton_Click;
                this.Controls.Add(createBoardButton);
            }
        }

        private void LoadBoards(FlowLayoutPanel boardsPanel)
        {
            if (_currentUser == null)
            {
                MessageBox.Show("User data is not available.");
                return;
            }

            var boards = _boardRepository.GetBoards(_currentUser.Id);
            if (!boards.Any())
            {
                var noBoardsLabel = new Label
                {
                    Text = "No boards available.",
                    AutoSize = true,
                    ForeColor = Color.Gray
                };
                boardsPanel.Controls.Add(noBoardsLabel);
            }
            else
            {
                foreach (var board in boards)
                {
                    var boardButton = new Button
                    {
                        Text = board.BoardName,
                        Size = new Size(200, 50),
                        Tag = board.Id
                    };
                    boardButton.Click += BoardButton_Click;
                    boardsPanel.Controls.Add(boardButton);
                }
            }
        }

        private void LogoutButton_Click(object sender, EventArgs e)
        {
            var loginForm = _serviceProvider.GetRequiredService<LogInForm>();
            loginForm.Show();
            this.Close();
        }

        private void ProfileButton_Click(object sender, EventArgs e)
        {
            var profileForm = _serviceProvider.GetRequiredService<ProfileForm>();
            //profileForm.SetCurrentUser(_currentUser);
            profileForm.Show();
            this.Hide();
        }

        private void BoardButton_Click(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button?.Tag is int boardId)
            {
                var boardForm = _serviceProvider.GetRequiredService<BoardForm>();
                //boardForm.SetBoardId(boardId);
                boardForm.Show();
                this.Hide();
            }
        }

        private void CreateBoardButton_Click(object sender, EventArgs e)
        {
            var createBoardForm = _serviceProvider.GetRequiredService<CreateBoardForm>();
            //createBoardForm.SetUserId(_currentUser.Id);
            //createBoardForm.BoardCreated += (s, args) =>
            {
                var boardsPanel = this.Controls.Find("boardsPanel", true).FirstOrDefault() as FlowLayoutPanel;
                LoadBoards(boardsPanel);
            };
            createBoardForm.Show();
        }
    }
}