using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace EmployeeManagementSystem
{
    public partial class FormRed : Form
    {
        private int employeeId;
        public FormRed(int id)
        {
            InitializeComponent();
            employeeId = id;
            LoadEmployeeData();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
        private void LoadEmployeeData()
        {
            using (SqlConnection connection = new SqlConnection("Server=DESKTOP-M6PNEH5\\UHHH;Database=UniversityDB;Integrated Security=True;"))
            {
                connection.Open();
                string query = "SELECT FirstName, LastName, MiddleName, Age, Phone, Position, Login, Password, Avatar FROM Employees WHERE Id = @id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", employeeId);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read()) // Проверяем, были ли данные загружены
                {
                    textBox1.Text = reader["FirstName"].ToString();
                    textBox2.Text = reader["LastName"].ToString();
                    textBox3.Text = reader["MiddleName"].ToString();
                    numericUpDown1.Value = Convert.ToInt32(reader["Age"]);
                    textBox5.Text = reader["Phone"].ToString();
                    textBox8.Text = reader["Password"].ToString(); // Заполните поле пароля, если оно есть.
                }
                else
                {
                    MessageBox.Show("Данные не найдены!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection("Server=DESKTOP-M6PNEH5\\UHHH;Database=UniversityDB;Integrated Security=True;"))
            {
                connection.Open();
                string query = "UPDATE Employees SET FirstName = @firstName, LastName = @lastName, MiddleName = @middleName, " +
                               "Age = @age, Phone = @phone, Position = @position, Password = @password WHERE Id = @id";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@firstName", textBox1.Text);
                command.Parameters.AddWithValue("@lastName", textBox2.Text);
                command.Parameters.AddWithValue("@middleName", textBox3.Text);
                command.Parameters.AddWithValue("@age", numericUpDown1.Value); // Добавлено
                command.Parameters.AddWithValue("@phone", textBox5.Text);
                command.Parameters.AddWithValue("@position", textBox4.Text); // Подразумевается, что поле для должности есть
                command.Parameters.AddWithValue("@password", textBox8.Text); // Убедитесь, что поле для пароля есть
                command.Parameters.AddWithValue("@id", employeeId); // Убедитесь, что employeeId инициализирован

                command.ExecuteNonQuery();
                MessageBox.Show("Данные обновлены!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void FormRed_Load(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}