using Microsoft.Extensions.DependencyInjection;
using Project_Q4.Models;
using Project_Q4.Services;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Project_Q4.Forms
{
    public partial class BoardForm : Form
    {
        private readonly BoardRepository _boardRepository;
        private readonly TaskRepository _taskRepository;
        private readonly IServiceProvider _serviceProvider;
        private Panel _boardPanel;
        private Panel _chatPanel;
        private Point _lastMousePosition;
        private bool _isDragging = false;
        private User _currentUser;
        private Panel _selectedTaskPanel;
        private bool _isDraggingTask = false;
        private Point _lastMousePositionOnTask;
        private bool _isEditingTask = false;
        private int _currentBoardId;

        public BoardForm(BoardRepository boardRepository, TaskRepository taskRepository, IServiceProvider serviceProvider)
        {
            _boardRepository = boardRepository;
            _taskRepository = taskRepository;
            _serviceProvider = serviceProvider;
            InitializeComponent();
        }

        public void SetCurrentUser(User user)
        {
            _currentUser = user;
        }

        public void SetBoardId(int boardId)
        {
            var board = _boardRepository.GetBoard(boardId);
            if (board == null)
            {
                MessageBox.Show("Board not found.");
                this.Close();
                return;
            }

            this.Text = board.BoardName;
            _currentBoardId = boardId;
            InitializeUI(board);
        }

        private void InitializeUI(Board board)
        {
            this.Size = new Size(1000, 600);

            var headerPanel = new Panel
            {
                Location = new Point(0, 0),
                Size = new Size(this.ClientSize.Width - 200, 50),
                BackColor = Color.LightGray
            };
            this.Controls.Add(headerPanel);

            var boardNameLabel = new Label
            {
                Text = board.BoardName,
                Location = new Point(10, 10),
                AutoSize = true,
                Font = new Font("Arial", 12, FontStyle.Bold)
            };
            headerPanel.Controls.Add(boardNameLabel);

            var addTaskButton = new Button
            {
                Text = "Add Task",
                Location = new Point(headerPanel.Width - 200, 10),
                Size = new Size(80, 30)
            };
            addTaskButton.Click += AddTaskButton_Click;
            headerPanel.Controls.Add(addTaskButton);

            var backToMainFormButton = new Button
            {
                Text = "Back To Main",
                Location = new Point(headerPanel.Width - 100, 10),
                Size = new Size(100, 30)
            };
            backToMainFormButton.Click += (sender, e) =>
            {
                var mainForm = _serviceProvider.GetRequiredService<MainForm>();
                mainForm.SetCurrentUser(_currentUser);
                mainForm.Show();
                this.Close();
            };
            headerPanel.Controls.Add(backToMainFormButton);

            _chatPanel = new Panel
            {
                Location = new Point(this.ClientSize.Width - 200, 50),
                Size = new Size(200, this.ClientSize.Height - 50),
                BackColor = Color.LightBlue
            };
            this.Controls.Add(_chatPanel);

            _boardPanel = new Panel
            {
                Location = new Point(0, 50),
                Size = new Size(this.ClientSize.Width - 200, this.ClientSize.Height - 50),
                BackColor = Color.Gray,
                AutoScroll = true
            };
            this.Controls.Add(_boardPanel);

            _boardPanel.MouseDown += BoardPanel_MouseDown;
            _boardPanel.MouseMove += BoardPanel_MouseMove;
            _boardPanel.MouseUp += BoardPanel_MouseUp;
            _boardPanel.Click += BoardPanel_Click;

            LoadTasks(board.Id);
        }

        private void LoadTasks(int boardId)
        {
            var tasks = _taskRepository.GetTasksByBoardId(boardId);
            foreach (var task in tasks)
            {
                CreateTaskControl(task);
            }
        }

        private void AddTaskButton_Click(object sender, EventArgs e)
        {
            float centerX = (_boardPanel.Width - 200) / 2;
            float centerY = (_boardPanel.Height - 200) / 2;

            var task = _taskRepository.CreateTask(
                boardId: _currentBoardId,
                x: centerX,
                y: centerY
            );

            CreateTaskControl(task);
        }

        private void CreateTaskControl(Models.Task task)
        {
            var taskPanel = new Panel
            {
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Width = 200,
                Height = 150,
                Location = new Point((int)task.PositionX, (int)task.PositionY),
                Tag = task.Id
            };

            var titleLabel = new Label
            {
                Text = string.IsNullOrEmpty(task.Title) ? "No Title" : task.Title,
                Location = new Point(10, 10),
                AutoSize = true,
                Font = new Font("Arial", 10, FontStyle.Bold)
            };
            taskPanel.Controls.Add(titleLabel);

            var descriptionLabel = new Label
            {
                Text = string.IsNullOrEmpty(task.Description) ? "No Description" : task.Description,
                Location = new Point(10, 40),
                AutoSize = true
            };
            taskPanel.Controls.Add(descriptionLabel);

            var priorityLabel = new Label
            {
                Text = $"Priority: {task.Priority}",
                Location = new Point(10, 70),
                AutoSize = true
            };
            taskPanel.Controls.Add(priorityLabel);

            var statusLabel = new Label
            {
                Text = $"Status: {task.Status}",
                Location = new Point(10, 90),
                AutoSize = true
            };
            taskPanel.Controls.Add(statusLabel);

            var deadlineLabel = new Label
            {
                Text = $"Deadline: {task.Deadline?.ToString("dd.MM.yyyy") ?? "No Deadline"}",
                Location = new Point(10, 110),
                AutoSize = true
            };
            taskPanel.Controls.Add(deadlineLabel);

            var rewardLabel = new Label
            {
                Text = $"Reward: {task.Reward}",
                Location = new Point(10, 130),
                AutoSize = true
            };
            taskPanel.Controls.Add(rewardLabel);

            _boardPanel.Controls.Add(taskPanel);

            taskPanel.Click += TaskPanel_Click;
            taskPanel.MouseDown += TaskPanel_MouseDown;
            taskPanel.MouseMove += TaskPanel_MouseMove;
            taskPanel.MouseUp += TaskPanel_MouseUp;
            taskPanel.DoubleClick += TaskPanel_DoubleClick;
        }

        private void TaskPanel_DoubleClick(object sender, EventArgs e)
        {
            _isEditingTask = true;

            var taskPanel = sender as Panel;

            taskPanel.Controls.Clear();

            var taskId = (int)taskPanel.Tag;
            var task = _taskRepository.GetTasksByBoardId(_currentBoardId).FirstOrDefault(t => t.Id == taskId);
            if (task == null) return;

            var titleTextBox = new TextBox
            {
                Text = task.Title,
                Location = new Point(10, 10),
                Width = taskPanel.Width - 20,
                Multiline = true,
                AutoSize = true
            };
            taskPanel.Controls.Add(titleTextBox);

            var descriptionTextBox = new TextBox
            {
                Text = task.Description,
                Location = new Point(10, titleTextBox.Bottom + 10),
                Width = taskPanel.Width - 20,
                Multiline = true,
                AutoSize = true
            };
            taskPanel.Controls.Add(descriptionTextBox);

            var priorityComboBox = new ComboBox
            {
                Items = { "Low", "Medium", "High" },
                SelectedItem = task.Priority.ToString(),
                Location = new Point(10, descriptionTextBox.Bottom + 10),
                Width = taskPanel.Width - 20
            };
            taskPanel.Controls.Add(priorityComboBox);

            var statusComboBox = new ComboBox
            {
                Items = { "Free", "InProgress", "Completed" },
                SelectedItem = task.Status.ToString(),
                Location = new Point(10, priorityComboBox.Bottom + 10),
                Width = taskPanel.Width - 20
            };
            taskPanel.Controls.Add(statusComboBox);

            var deadlinePicker = new DateTimePicker
            {
                Value = task.Deadline ?? DateTime.Now,
                Format = DateTimePickerFormat.Short,
                Location = new Point(10, statusComboBox.Bottom + 10),
                Width = taskPanel.Width - 20
            };
            taskPanel.Controls.Add(deadlinePicker);

            var rewardTextBox = new TextBox
            {
                Text = task.Reward.ToString(),
                Location = new Point(10, deadlinePicker.Bottom + 10),
                Width = taskPanel.Width - 20
            };
            taskPanel.Controls.Add(rewardTextBox);

            taskPanel.Height = rewardTextBox.Bottom + 10;

            taskPanel.Click += (s, ev) =>
            {
                task.Title = titleTextBox.Text;
                task.Description = descriptionTextBox.Text;

                if (Enum.TryParse(priorityComboBox.SelectedItem?.ToString(), out Models.TaskPriority priority))
                {
                    task.Priority = priority;
                }

                if (Enum.TryParse(statusComboBox.SelectedItem?.ToString(), out Models.TaskStatus status))
                {
                    task.Status = status;
                }

                task.Deadline = deadlinePicker.Value;
                task.Reward = int.TryParse(rewardTextBox.Text, out int reward) ? reward : 0;

                // Сохраняем изменения в базу данных
                _taskRepository.UpdateTask(task);

                // Удаляем старую панель задачи и создаем новую
                _boardPanel.Controls.Remove(taskPanel);
                CreateTaskControl(task);

                // Выходим из режима редактирования
                _isEditingTask = false;
            };
        }

        private void TaskPanel_Click(object sender, EventArgs e)
        {
            var taskPanel = sender as Panel;

            if (_selectedTaskPanel != null && _selectedTaskPanel != taskPanel)
            {
                _selectedTaskPanel.BackColor = Color.White;
            }

            _selectedTaskPanel = taskPanel;
            _selectedTaskPanel.BackColor = Color.LightGray;
        }

        private void TaskPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _isDraggingTask = true;
                _lastMousePositionOnTask = Cursor.Position;
            }
        }

        private void TaskPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDraggingTask)
            {
                var taskPanel = sender as Panel;

                int deltaX = Cursor.Position.X - _lastMousePositionOnTask.X;
                int deltaY = Cursor.Position.Y - _lastMousePositionOnTask.Y;

                taskPanel.Location = new Point(
                    taskPanel.Location.X + deltaX,
                    taskPanel.Location.Y + deltaY
                );

                _lastMousePositionOnTask = Cursor.Position;
            }
        }

        private void TaskPanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (_isDraggingTask)
            {
                _isDraggingTask = false;

                var taskPanel = sender as Panel;
                var taskId = (int)taskPanel.Tag;
                var task = _taskRepository.GetTasksByBoardId(_currentBoardId).FirstOrDefault(t => t.Id == taskId);
                if (task != null)
                {
                    task.PositionX = taskPanel.Location.X;
                    task.PositionY = taskPanel.Location.Y;
                    _taskRepository.UpdateTask(task);
                }
            }
        }

        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, Keys keyData)
        {
            if (_selectedTaskPanel != null && !_isEditingTask && (keyData == Keys.Delete || keyData == Keys.Back))
            {
                var taskId = (int)_selectedTaskPanel.Tag;
                _taskRepository.DeleteTask(taskId);
                _boardPanel.Controls.Remove(_selectedTaskPanel);
                _selectedTaskPanel = null;
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void BoardPanel_Click(object sender, EventArgs e)
        {
            if (_selectedTaskPanel != null)
            {
                // Если задача находится в режиме редактирования, сохраняем изменения
                var taskId = (int)_selectedTaskPanel.Tag;
                var task = _taskRepository.GetTasksByBoardId(_currentBoardId).FirstOrDefault(t => t.Id == taskId);
                if (task != null)
                {
                    // Получаем данные из текстовых полей и выпадающих списков
                    foreach (var control in _selectedTaskPanel.Controls)
                    {
                        if (control is TextBox titleTextBox && titleTextBox.Location.Y == 10)
                        {
                            task.Title = titleTextBox.Text;
                        }
                        else if (control is TextBox descriptionTextBox && descriptionTextBox.Location.Y == 40)
                        {
                            task.Description = descriptionTextBox.Text;
                        }
                        else if (control is ComboBox priorityComboBox && priorityComboBox.Location.Y == 70)
                        {
                            if (Enum.TryParse(priorityComboBox.SelectedItem?.ToString(), out Models.TaskPriority priority))
                            {
                                task.Priority = priority;
                            }
                        }
                        else if (control is ComboBox statusComboBox && statusComboBox.Location.Y == 90)
                        {
                            if (Enum.TryParse(statusComboBox.SelectedItem?.ToString(), out Models.TaskStatus status))
                            {
                                task.Status = status;
                            }
                        }
                        else if (control is DateTimePicker deadlinePicker && deadlinePicker.Location.Y == 110)
                        {
                            task.Deadline = deadlinePicker.Value;
                        }
                        else if (control is TextBox rewardTextBox && rewardTextBox.Location.Y == 130)
                        {
                            task.Reward = int.TryParse(rewardTextBox.Text, out int reward) ? reward : 0;
                        }
                    }

                    // Сохраняем изменения в базу данных
                    _taskRepository.UpdateTask(task);

                    // Пересоздаем задачу в обычном режиме
                    _boardPanel.Controls.Remove(_selectedTaskPanel);
                    CreateTaskControl(task);

                    // Снимаем выделение с задачи
                    _selectedTaskPanel = null;

                    // Выходим из режима редактирования
                    _isEditingTask = false;
                }
            }
        }

        private void BoardPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _isDragging = true;
                _lastMousePosition = e.Location;
            }
        }

        private void BoardPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDragging)
            {
                int deltaX = e.X - _lastMousePosition.X;
                int deltaY = e.Y - _lastMousePosition.Y;

                _boardPanel.AutoScrollPosition = new Point(
                    -_boardPanel.AutoScrollPosition.X - deltaX,
                    -_boardPanel.AutoScrollPosition.Y - deltaY
                );

                _lastMousePosition = e.Location;
            }
        }

        private void BoardPanel_MouseUp(object sender, MouseEventArgs e)
        {
            _isDragging = false;
        }
    }
}