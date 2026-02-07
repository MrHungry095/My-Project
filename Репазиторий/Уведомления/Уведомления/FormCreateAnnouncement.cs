using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Уведомления
{
    public partial class FormCreateAnnouncement : Form
    {
        private int userId;

        public FormCreateAnnouncement(int userId)
        {
            InitializeComponent();
            this.userId = userId;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
               

                using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-M6PNEH5\\UHHH;Initial Catalog=EmployeeAnnouncements;Integrated Security=True"))
                {
                    conn.Open();

                    // Проверка существования пользователя
                    using (SqlCommand checkUserCmd = new SqlCommand("SELECT COUNT(*) FROM Users WHERE UserId = @userId", conn))
                    {
                        checkUserCmd.Parameters.AddWithValue("@userId", userId);
                        int userExists = (int)checkUserCmd.ExecuteScalar();

                        if (userExists == 0)
                        {
                            MessageBox.Show("Пользователь не найден.");
                            return;
                        }
                    }

                    // Вставка нового объявления
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Announcements (Title, Content, Type, AuthorId) VALUES (@title, @content, @type, @authorId)", conn))
                    {
                        cmd.Parameters.AddWithValue("@title", textBox1.Text);
                        cmd.Parameters.AddWithValue("@content", textBox2.Text);
                        cmd.Parameters.AddWithValue("@type", textBox3.Text);
                        cmd.Parameters.AddWithValue("@authorId", userId);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Объявление создано");
                        this.Hide();
                        FormAdmin adminForm = new FormAdmin(userId);
                        adminForm.Show();
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("SQL Ошибка: " + sqlEx.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form formA = new FormAdmin(userId);
            formA.Show();
            this.Close();
        }
    }
}