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
    public partial class FormRegister : Form
    {
        private int userId;
        public FormRegister()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-M6PNEH5\\UHHH;Initial Catalog=EmployeeAnnouncements;Integrated Security=True"))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Users (FirstName, LastName, Patronymic, PhoneNumber, Login, Password, Role) VALUES (@firstName, @lastName, @patronymic, @phoneNumber, @login, @password, 'user')", conn);
                cmd.Parameters.AddWithValue("@firstName", textBox1.Text);
                cmd.Parameters.AddWithValue("@lastName", textBox2.Text);
                cmd.Parameters.AddWithValue("@patronymic", textBox3.Text);
                cmd.Parameters.AddWithValue("@phoneNumber", textBox4.Text);
                cmd.Parameters.AddWithValue("@login", textBox5.Text);
                cmd.Parameters.AddWithValue("@password", textBox6.Text);

                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Регистрация прошла успешно");
                    this.Hide();
                    Form formAD = new FormAdmin(userId);
                    formAD.Show();
                    this.Hide();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form formAD = new FormUserManagement();
            formAD.Show();
            this.Hide();
        }
    }
}
