using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Уведомления
{
    public partial class FormUserManagement : Form
    {
        private int userId;

        public FormUserManagement()
        {
            InitializeComponent();
            LoadUsers();
        }

        private void FormUserManagement_Load(object sender, EventArgs e)
        {
            LoadUsers(); // Загружаем пользователей при загрузке формы
        }

        private void LoadUsers()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-M6PNEH5\\UHHH;Initial Catalog=EmployeeAnnouncements;Integrated Security=True"))
                {
                    conn.Open();
                    // Используем корректные имена столбцов
                    SqlCommand cmd = new SqlCommand("SELECT UserId, FirstName, LastName, Patronymic, PhoneNumber AS Phone, Login, Password FROM Users", conn);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Устанавливаем источник данных для DataGridView
                    if (dataTable.Rows.Count > 0)
                    {
                        dataGridView1.DataSource = dataTable;
                    }
                    else
                    {
                        MessageBox.Show("Нет пользователей для отображения.");
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Ошибка при загрузке данных: " + ex.Message); // Вывод ошибки
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form formФФФ = new FormAdmin(userId);
            formФФФ.ShowDialog();
            this.Close();
        }

       

       
        private void button1_Click_1(object sender, EventArgs e)
        {
            Form formкук = new FormRegister();
            formкук.ShowDialog();
            this.Close();

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int userId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["UserId"].Value); // Получаем UserId из выбранной строки

                using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-M6PNEH5\\UHHH;Initial Catalog=EmployeeAnnouncements;Integrated Security=True"))
                {
                    if (userId == 5 || userId == 3)
                    {
                        MessageBox.Show("Вы пытаетесь удалить администратора, администарторов удалять нельзя");
                    }
                    else
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("DELETE FROM Users WHERE UserId = @UserId", conn);
                        cmd.Parameters.AddWithValue("@UserId", userId); // Передаем UserId в параметры
                        cmd.ExecuteNonQuery(); // Выполняем команду
                        MessageBox.Show("Пользователь успешно удален.");
                    }
                  
                   

                    
                }
               

                LoadUsers(); // Обновляем список пользователей после удаления
            }
            else
            {
                MessageBox.Show("Выберите пользователя для удаления.");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridView1.SelectedRows[0];
                userId = Convert.ToInt32(selectedRow.Cells["UserId"].Value); // Сохраняем userId в переменной класса

                // Вы можете добавлять логику здесь, если необходимо
                MessageBox.Show("Выбранный UserId: " + userId);
            }
        }
    }
}