using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Уведомления
{
    public partial class FormAdmin : Form
    {
        private int userId;

        public FormAdmin(int userId)
        {
            InitializeComponent();
            this.userId = userId; // Сохраняем переданный userId
        }

        private void FormAdmin_Load(object sender, EventArgs e)
        {
            LoadAnnouncements(); // Загружаем данные объявлений при загрузке формы
        }

        private void LoadAnnouncements()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-M6PNEH5\\UHHH;Initial Catalog=EmployeeAnnouncements;Integrated Security=True"))
                {
                    conn.Open();
                    // Используем корректные имена столбцов
                    SqlCommand cmd = new SqlCommand("SELECT AnnouncementId, Title, Content, Type, CreationDate FROM Announcements", conn);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dataGridView1.DataSource = dataTable; // Устанавливаем источник данных для DataGridView
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Ошибка при загрузке данных: " + ex.Message); // Вывод ошибки
            }
        }

        

        private void DeleteAnnouncement(int announceId)
        {
            using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-M6PNEH5\\UHHH;Initial Catalog=EmployeeAnnouncements;Integrated Security=True"))
            {
                conn.Open();
                // Удаляем по корректному идентификатору
                SqlCommand cmd = new SqlCommand("DELETE FROM Announcements WHERE AnnouncementId = @AnnouncementId", conn);
                cmd.Parameters.AddWithValue("@AnnouncementId", announceId);
                cmd.ExecuteNonQuery();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form formAdd = new FormCreateAnnouncement(userId);
            formAdd.ShowDialog();
            LoadAnnouncements();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var result = MessageBox.Show("Вы уверены, что хотите удалить это объявление?", "Подтверждение удаления", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    // Получаем ID выбранного объявления
                    int announceId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["AnnouncementId"].Value);
                    DeleteAnnouncement(announceId); // Удаляем объявление
                    LoadAnnouncements(); // Обновляем список объявлений
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите объявление для удаления.");
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form formL = new FormLogin();
            formL.ShowDialog();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form formП = new FormUserManagement();
            formП.ShowDialog();
            this.Close();
        }
    }
}