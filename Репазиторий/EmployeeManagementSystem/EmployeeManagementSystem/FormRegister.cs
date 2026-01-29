using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace EmployeeManagementSystem
{
    public partial class FormRegister : Form
    {
        private string avatarPath;

        public FormRegister()
        {

            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string enteredLogin = textBox7.Text.Trim(); // Логин из текстового поля

            using (SqlConnection connection = new SqlConnection("Server=DESKTOP-M6PNEH5\\UHHH;Database=UniversityDB;Integrated Security=True;"))
            {
                connection.Open();

                // Проверка на существующий логин
                string checkQuery = "SELECT COUNT(*) FROM Employees WHERE Login = @login";
                SqlCommand checkCommand = new SqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@login", enteredLogin);

                int count = (int)checkCommand.ExecuteScalar();
                if (count > 0)
                {
                    MessageBox.Show("Логин уже существует. Пожалуйста, выберите другой логин.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Прекращаем выполнение метода, если логин занят
                }

                // Если логин уникален, выполняем вставку нового сотрудника
                string insertQuery = "INSERT INTO Employees (FirstName, LastName, MiddleName, Age, Phone, Position, Login, Password, Avatar) " +
                                     "VALUES (@firstName, @lastName, @middleName, @age, @phone, @position, @login, @password, @avatar)";

                SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("@firstName", textBox1.Text);
                insertCommand.Parameters.AddWithValue("@lastName", textBox2.Text);
                insertCommand.Parameters.AddWithValue("@middleName", textBox3.Text);
                insertCommand.Parameters.AddWithValue("@age", Convert.ToInt32(numAge.Value));
                insertCommand.Parameters.AddWithValue("@phone", textBox5.Text);
                insertCommand.Parameters.AddWithValue("@position", textBox6.Text);
                insertCommand.Parameters.AddWithValue("@login", enteredLogin);
                insertCommand.Parameters.AddWithValue("@password", textBox8.Text);

                // Проверка, был ли выбран аватар
                if (string.IsNullOrEmpty(avatarPath))
                {
                    insertCommand.Parameters.AddWithValue("@avatar", DBNull.Value); // или "default.png", если у вас есть изображение по умолчанию
                }
                else
                {
                    insertCommand.Parameters.AddWithValue("@avatar", avatarPath);
                }

                insertCommand.ExecuteNonQuery();
                MessageBox.Show("Сотрудник зарегистрирован!");

                Form1 loginForm = new Form1();
                loginForm.Show(); // Открываем окно входа
                this.Hide();
            }
        }
        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "Image Files (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp";
                openFileDialog.Title = "Выберите аватар";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedFilePath = openFileDialog.FileName;

                    string extension = Path.GetExtension(selectedFilePath).ToLower();
                    if (extension == ".jpg" || extension == ".jpeg" || extension == ".png" || extension == ".bmp")
                    {
                        pictureBox1.ImageLocation = selectedFilePath;
                        pictureBox1.SizeMode = PictureBoxSizeMode.Zoom; 
                        avatarPath = selectedFilePath;
                    }
                    else
                    {
                        MessageBox.Show("Пожалуйста, выберите файл изображения (.jpg, .jpeg, .png, .bmp).", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 loginForm = new Form1();
            loginForm.Show();
            this.Hide();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void numAge_ValueChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }
    }
}