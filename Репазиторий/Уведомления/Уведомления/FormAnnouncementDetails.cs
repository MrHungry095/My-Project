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
    
    public partial class FormAnnouncementDetails : Form
    {
        private int userId;
        private string title;
        public FormAnnouncementDetails(string title)
        {
            InitializeComponent();
            this.title = title;
            LoadDetails();
        }

        private void LoadDetails()
        {
            using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-M6PNEH5\\UHHH;Initial Catalog=EmployeeAnnouncements;Integrated Security=True"))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT Content, Type, CreationDate FROM Announcements WHERE Title=@title", conn);
                cmd.Parameters.AddWithValue("@title", title);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    textBox2.Text = reader.GetString(0);
                    label2.Text = reader.GetString(1);
                    label3.Text = reader.GetDateTime(2).ToString("g");
                    label4.Text = title;

                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Form formAnn = new FormAnnouncements(userId);
            formAnn.ShowDialog();
            this.Close();   
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
