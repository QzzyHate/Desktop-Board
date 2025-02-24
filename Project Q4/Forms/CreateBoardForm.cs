using System;
using System.Windows.Forms;
using Project_Q4.Models;
using Project_Q4.Services;

namespace Project_Q4.Forms
{
    public partial class CreateBoardForm : Form
    {
        private readonly BoardRepository _boardRepository;
        private User _currentUser;

        public event EventHandler BoardCreated;

        public CreateBoardForm(BoardRepository boardRepository)
        {
            InitializeUI();
            _boardRepository = boardRepository;
        }

        public void SetCurrentUser(User user)
        {
            _currentUser = user;
        }

        private void InitializeUI()
        {
            this.Text = "Create New Board";
            this.Size = new System.Drawing.Size(300, 200);

            var nameLabel = new Label
            {
                Text = "Board Name:",
                Location = new System.Drawing.Point(10, 20),
                AutoSize = true
            };
            this.Controls.Add(nameLabel);

            var nameTextBox = new TextBox
            {
                Location = new System.Drawing.Point(10, 50),
                Size = new System.Drawing.Size(260, 20)
            };
            this.Controls.Add(nameTextBox);

            var createButton = new Button
            {
                Text = "Create Board",
                Location = new System.Drawing.Point(10, 90),
                Size = new System.Drawing.Size(100, 30)
            };
            createButton.Click += (sender, e) =>
            {
                var boardName = nameTextBox.Text.Trim();
                if (string.IsNullOrEmpty(boardName))
                {
                    MessageBox.Show("Please enter a board name.");
                    return;
                }

                try
                {
                    _boardRepository.CreateBoard(boardName, _currentUser); //Передаем полный объект пользователя
                    MessageBox.Show("Board created successfully!");

                    //Уведомляем о создании новой доски
                    BoardCreated?.Invoke(this, EventArgs.Empty);

                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error creating board: {ex.Message}");
                }
            };
            this.Controls.Add(createButton);
        }
    }
}