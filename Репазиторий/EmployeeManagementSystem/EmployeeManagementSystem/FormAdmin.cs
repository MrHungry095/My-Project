using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace EmployeeManagementSystem
{
    public partial class FormAdmin : Form
    {
        private SqlConnection connection;

        public FormAdmin()
        {
            InitializeComponent();
            LoadEmployees();
        }

        private void LoadEmployees(string searchQuery = "")
        {
            try
            {
                connection = new SqlConnection("Server=DESKTOP-M6PNEH5\\UHHH;Database=UniversityDB;Integrated Security=True;");
                connection.Open();

                string query = "SELECT Id, FirstName, LastName, MiddleName, Age, Phone, Position, Login, Password, Avatar FROM Employees";

                if (!string.IsNullOrWhiteSpace(searchQuery))
                {
                    query += " WHERE FirstName LIKE @search OR LastName LIKE @search OR MiddleName LIKE @search OR Age LIKE @search OR Phone LIKE @search OR Position LIKE @search OR Login LIKE @search";//по каким параметрам искать
                }

                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                if (!string.IsNullOrWhiteSpace(searchQuery))
                {
                    adapter.SelectCommand.Parameters.AddWithValue("@search", "%" + searchQuery + "%");
                }

                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                // Убедитесь, что DataGridView называется dataGridView1
                dataGridView1.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке данных: " + ex.Message);
            }
            finally
            {
                connection?.Close();
            }
        }

        private void DeleteEmployee(int employeeId)
        {
            try
            {
                connection = new SqlConnection("Server=DESKTOP-M6PNEH5\\UHHH;Database=UniversityDB;Integrated Security=True;");
                connection.Open();
                string query = "DELETE FROM Employees WHERE Id = @id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", employeeId);
                command.ExecuteNonQuery();
                MessageBox.Show("Сотрудник удалён!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при удалении сотрудника: " + ex.Message);
            }
            finally
            {
                connection?.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var formRegister = new FormRegister();
            formRegister.ShowDialog();
            LoadEmployees();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int employeeId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Id"].Value);
                var result = MessageBox.Show("Вы уверены, что хотите удалить этого сотрудника?", "Подтверждение", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    DeleteEmployee(employeeId);
                    LoadEmployees();
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите сотрудника для удаления.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int employeeId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Id"].Value);
                var formProfile = new FormProfile(employeeId);
                formProfile.ShowDialog();
                LoadEmployees();
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите сотрудника для редактирования.");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form1 loginForm = new Form1();
            loginForm.Show();
            this.Hide();
        }

        private void FormAdmin_Load(object sender, EventArgs e)
        {
            // Место для события загрузки формы
        }

        private void button5_Click(object sender, EventArgs e)
        {
            LoadEmployees();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string searchQuery = textBox1.Text.Trim();
            LoadEmployees(searchQuery);
        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}