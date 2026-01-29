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
    public partial class FormProfile : Form
    {
        private int employeeId;
        private string avatarPath;

        public FormProfile(int id)
        {
            Button btnDocuments = new Button();
            btnDocuments.Text = "My Documents";
            btnDocuments.Location = new Point(20, 400); // Adjust location as needed
            btnDocuments.Click += button2_Click;
            InitializeComponent();
            employeeId = id;
            LoadEmployeeData();
        }
        private void LoadEmployeeData()
        {
            SqlConnection connection = null; // Объявляем переменную для подключения

            try
            {
                connection = new SqlConnection("Server=DESKTOP-M6PNEH5\\UHHH;Database=UniversityDB;Integrated Security=True;");
                connection.Open();
                string query = "SELECT FirstName, LastName, MiddleName, Age, Phone, Position, Login, Password, Avatar FROM Employees WHERE Id = @id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", employeeId);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    label9.Text = reader["FirstName"].ToString();
                    label10.Text = reader["LastName"].ToString();
                    label11.Text = reader["MiddleName"].ToString();
                    numAge.Value = Convert.ToInt32(reader["Age"]);
                    label12.Text = reader["Phone"].ToString();
                    label7.Text = reader["Position"].ToString();
                    label14.Text = reader["Login"].ToString();
                    label15.Text = reader["Password"].ToString();
                    string avatarPath = reader["Avatar"].ToString();

                    // Проверка наличия файла и установка изображения
                    if (File.Exists(avatarPath))
                    {
                      pictureBox1.ImageLocation = avatarPath; // Показать аватар
                        pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                    else
                    {
                    
                        // Опционально: показать изображение по умолчанию, если аватар отсутствует
                        pictureBox1.ImageLocation = "path_to_default_image.png"; // Укажите путь к изображению по умолчанию
                    }

                }
            }
            finally { }

        }
        private void ProfileForm_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(avatarPath) && File.Exists(avatarPath))
            {
                pictureBox1.ImageLocation = avatarPath; // Убедитесь, что avatarPath содержит валидный путь
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormRed editForm = new FormRed(employeeId); // Передаем идентификатор
            editForm.Show();
        }
        private void UpdateAvatarPathInDatabase()
        {
            using (SqlConnection connection = new SqlConnection("Server=DESKTOP-M6PNEH5\\UHHH;Database=UniversityDB;Integrated Security=True;"))
            {
                connection.Open();
                string query = "UPDATE Employees SET Avatar = @avatar WHERE Id = @id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@avatar", avatarPath);
                command.Parameters.AddWithValue("@id", employeeId);

                command.ExecuteNonQuery();
                MessageBox.Show("Аватар обновлен!");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 loginForm = new Form1();
            loginForm.Show(); 
            this.Hide();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.InitialDirectory = "c:\\"; // Установите начальный каталог
                    openFileDialog.Filter = "Image Files (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp"; // Фильтр для изображений
                    openFileDialog.Title = "Выберите новый аватар";

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string selectedFilePath = openFileDialog.FileName;

                        // Проверяем, что файл является изображением
                        string extension = Path.GetExtension(selectedFilePath).ToLower();
                        if (extension == ".jpg" || extension == ".jpeg" || extension == ".png" || extension == ".bmp")
                        {
                            // Устанавливаем новое изображение на PictureBox
                            pictureBox1.ImageLocation = selectedFilePath;
                            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                            // Сохраняем новый путь к аватару
                            avatarPath = selectedFilePath;

                            // Обновляем путь к аватару в базе данных
                            UpdateAvatarPathInDatabase();
                        }
                        else
                        {
                            MessageBox.Show("Пожалуйста, выберите корректный файл изображения.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void FormProfile_Load(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormDocuments formDocs = new FormDocuments(employeeId);
            formDocs.ShowDialog();
        }

        private void label16_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
    }

