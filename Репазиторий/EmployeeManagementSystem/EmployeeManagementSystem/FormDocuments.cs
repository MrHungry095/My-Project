using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace EmployeeManagementSystem
{
    public partial class FormDocuments : Form
    {
        private int employeeId;

        public FormDocuments(int empId)
        {
            InitializeComponent();
            employeeId = empId;
            LoadDocuments();
            InitializeDocumentTypes();
        }

        private void InitializeDocumentTypes()
        {
            comboBox1.Items.Add("Паспорт");
            comboBox1.Items.Add("СНИЛС");
            comboBox1.Items.Add("Документ воинского учёта");
            comboBox1.Items.Add("Документ об образовании");
            comboBox1.Items.Add("Трудовая книжка");

        }

        private void LoadDocuments()
        {
            listView1.Items.Clear();
            using (SqlConnection connection = new SqlConnection("Server=DESKTOP-M6PNEH5\\UHHH;Database=UniversityDB;Integrated Security=True;"))
            {
                connection.Open();
                string query = "SELECT ed.DocumentTypeId, dt.TypeName, ed.FilePath " +
                               "FROM EmployeeDocuments ed " +
                               "INNER JOIN DocumentTypes dt ON ed.DocumentTypeId = dt.Id " +
                               "WHERE ed.EmployeeId = @id";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", employeeId);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ListViewItem item = new ListViewItem(reader["TypeName"].ToString());
                        item.SubItems.Add(reader["FilePath"].ToString());
                            listView1.Items.Add(item);
                    }
                }
                
                listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, выберите тип документа.");
                return;
            }

            using (OpenFileDialog openFile = new OpenFileDialog())
            {
                openFile.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

                if (openFile.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFile.FileName;
                    string docType = comboBox1.SelectedItem.ToString();

                    int documentTypeId;
                    bool typeFound = false;

                    using (SqlConnection connection = new SqlConnection("Server=DESKTOP-M6PNEH5\\UHHH;Database=UniversityDB;Integrated Security=True;"))
                    {
                        connection.Open();
                        string typeQuery = "SELECT Id FROM DocumentTypes WHERE TypeName = @typeName";
                        SqlCommand typeCmd = new SqlCommand(typeQuery, connection);
                        typeCmd.Parameters.AddWithValue("@typeName", docType);

                        object result = typeCmd.ExecuteScalar();
                        if (result != null)
                        {
                            documentTypeId = Convert.ToInt32(result);
                            typeFound = true;
                        }
                        else
                        {
                            MessageBox.Show("Тип документа не найден.");
                            return;
                        }

                        if (typeFound)
                        {
                            string insert = "INSERT INTO EmployeeDocuments (EmployeeId, DocumentTypeId, FilePath) VALUES (@empId, @type, @path)";
                            SqlCommand cmd = new SqlCommand(insert, connection);
                            cmd.Parameters.AddWithValue("@empId", employeeId);
                            cmd.Parameters.AddWithValue("@type", documentTypeId);
                            cmd.Parameters.AddWithValue("@path", filePath);

                            try
                            {
                                cmd.ExecuteNonQuery();
                                MessageBox.Show("Документ добавлен!");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Ошибка при добавлении документа: " + ex.Message);
                            }
                        }
                    }

                    LoadDocuments();
                }
            }
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (        listView1.SelectedItems.Count > 0)
            {
                string filePath = listView1.SelectedItems[0].SubItems[1].Text;

                // Отображение изображения в PictureBox
                try
                {

                    pictureBox1.Image = Image.FromFile(filePath);
                    pictureBox1.Visible = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при загрузке изображения: " + ex.Message);
                }
            }
        }
        private void FormDocuments_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
         
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listView1               .SelectedItems.Count > 0)
            {
                string filePath = listView1.SelectedItems[0].SubItems[1].Text;

                // Удаление документа из базы данных
                using (SqlConnection connection = new SqlConnection("Server=DESKTOP-M6PNEH5\\UHHH;Database=UniversityDB;Integrated Security=True;"))
                {
                    connection.Open();
                    string deleteQuery = "DELETE FROM EmployeeDocuments WHERE EmployeeId = @empId AND FilePath = @path";
                    SqlCommand cmd = new SqlCommand(deleteQuery, connection);
                    cmd.Parameters.AddWithValue("@empId", employeeId);
                    cmd.Parameters.AddWithValue("@path", filePath);
                    cmd.ExecuteNonQuery();
                }

                // Освобождаем изображение перед удалением файла
                if (pictureBox1.Image != null)
                {
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                    pictureBox1.Image.Dispose(); // Освобождаем ресурсы, связанные с изображением
                    pictureBox1.Image = null; // Устанавливаем в null, чтобы избежать проблем с ссылкой
                }

                // Удаление изображения из файловой системы (опционально)
                if (File.Exists(filePath))
                {
                    try
                    {
                        File.Delete(filePath);
                        MessageBox.Show("Документ удален!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка при удалении файла: " + ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Файл не найден, удаление не выполнено.");
                }

                LoadDocuments();
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите документ для удаления.");
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                string filePath = listView1.SelectedItems[0].SubItems[1].Text;

                // Выводим путь к файлу для отладки
                MessageBox.Show("Открываю файл: " + filePath);

                // Отображение изображения в PictureBox
                try
                {
                    // Проверяем, существует ли файл
                    if (File.Exists(filePath))
                    {
                        pictureBox1.Image?.Dispose(); // Освобождаем ресурсы от предыдущего изображения
                        pictureBox1.Image = Image.FromFile(filePath);
                        pictureBox1.SizeMode = PictureBoxSizeMode.Zoom; // Установка режима отображения
                        pictureBox1.Visible = true; // Убедитесь, что PictureBox видимый
                    }
                    else
                    {
                        MessageBox.Show("Файл не найден: " + filePath);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при загрузке изображения: " + ex.Message);
                }
            }
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }
    }
}




