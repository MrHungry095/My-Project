using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace EmployeeManagementSystem
{
    public partial class Form1 : Form
    {
        private const string AdminUsername = "admin"; // Предустановленный логин администратора
        private const string AdminPassword = "123456"; // Предустановленный пароль администратора

        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormRegister registerForm = new FormRegister();
            registerForm.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string enteredUsername = textBox2.Text; // Логин из текстового поля
            string enteredPassword = textBox1.Text; // Пароль из текстового поля

            // Проверка на логин и пароль администратора
            if (enteredUsername == AdminUsername && enteredPassword == AdminPassword)
            {
                MessageBox.Show("Добро пожаловать, администратор!");
                FormAdmin adminForm = new FormAdmin();
                adminForm.Show();
                this.Hide();
                return; // Прекращаем выполнение метода, если администратор вошел
            }

            using (SqlConnection connection = new SqlConnection("Server=DESKTOP-M6PNEH5\\UHHH;Database=UniversityDB;Integrated Security=True;"))
            {
                connection.Open();
                string query = "SELECT Id FROM Employees WHERE Login = @login AND Password = @password";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@login", enteredUsername);
                command.Parameters.AddWithValue("@password", enteredPassword);

                try
                {
                    var result = command.ExecuteScalar();
                    if (result != null)
                    {
                        int employeeId = (int)result;
                        FormProfile profileForm = new FormProfile(employeeId);
                        profileForm.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Неправильный логин или пароль!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при выполнении запроса: " + ex.Message);
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}

