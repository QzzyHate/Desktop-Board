using Project_Q4.Services;
using System.Windows.Forms;

namespace Project_Q4
{
    public partial class Form1 : Form
    {
        private readonly UserRepository _userRepository;

        public Form1(UserRepository userRepository)
        {
            InitializeComponent();
            _userRepository = userRepository;
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
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }
    }
}