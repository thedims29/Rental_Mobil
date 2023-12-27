using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SQLite;

namespace RentCar
{
    public partial class Login : Form
    {
        private SQLiteConnection koneksi;

        public Login()
        {
            InitializeComponent();
            koneksi = new SQLiteConnection("Data Source=D:\\22.11.4696\\RentalCar\\Data\\Rental.db;Version=3;"); // Sesuaikan dengan nama file database SQLite Anda
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query = "SELECT COUNT(*) FROM login WHERE Username = @username AND Password = @password";

            using (SQLiteCommand cmd = new SQLiteCommand(query, koneksi))
            {
                cmd.Parameters.AddWithValue("@username", textBox1.Text);
                cmd.Parameters.AddWithValue("@password", textBox2.Text);

                koneksi.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                koneksi.Close();

                if (count > 0)
                {
                    this.Hide();
                    Home panggil = new Home();
                    panggil.Show();
                }
                else
                {
                    MessageBox.Show("Mohon isi Username dan Password anda dengan benar!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox1.Focus();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}
