using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Уведомления
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

      
private void button1_Click(object sender, EventArgs e)
        {
            string login = textBox1.Text;
            string password = textBox2.Text;

            using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-M6PNEH5\\UHHH;Initial Catalog=EmployeeAnnouncements;Integrated Security=True"))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT UserId, Role FROM Users WHERE Login=@login AND Password=@password", conn);
                cmd.Parameters.AddWithValue("@login", login);
                cmd.Parameters.AddWithValue("@password", password);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    int userId = reader.GetInt32(0);
                    string role = reader.GetString(1);

                    if (role == "admin")
                    {
                        FormAdmin adminForm = new FormAdmin(userId);
                        adminForm.Show();
                    }
                    else
                    {
                        FormAnnouncements announcementsForm = new FormAnnouncements(userId);
                        announcementsForm.Show();
                    }
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Неправильный логин или пароль");
                }
            }
        }
    }
}
