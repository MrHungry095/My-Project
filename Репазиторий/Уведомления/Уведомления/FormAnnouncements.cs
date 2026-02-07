using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Уведомления
{
    public partial class FormAnnouncements : Form
    {
        private int userId;

        public FormAnnouncements(int userId)
        {
            InitializeComponent();
            this.userId = userId;
            LoadAnnouncements();
            textBox1.TextChanged += TextBoxSearch_TextChanged; // Подписка на событие изменения текста
        }

        private void LoadAnnouncements()
        {
            using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-M6PNEH5\\UHHH;Initial Catalog=EmployeeAnnouncements;Integrated Security=True"))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT Title FROM Announcements", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    listBox1.Items.Add(reader.GetString(0));
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                string title = listBox1.SelectedItem.ToString();
                FormAnnouncementDetails detailsForm = new FormAnnouncementDetails(title);
                detailsForm.Show();
                this.Hide();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form formL = new FormLogin();
            formL.ShowDialog();
            this.Close();
        }

        private void TextBoxSearch_TextChanged(object sender, EventArgs e)
        {
            string searchText = textBox1.Text.ToLower();
            listBox1.Items.Clear();
            LoadAnnouncements(); // Сначала загружаем все объявления

            // Фильтруем элементы
            for (int i = listBox1.Items.Count - 1; i >= 0; i--)
            {
                if (!listBox1.Items[i].ToString().ToLower().Contains(searchText))
                {
                    listBox1.Items.RemoveAt(i);
                }
            }
        }
    }
}