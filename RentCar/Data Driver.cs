using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace RentCar
{
    public partial class Data_Driver : Form
    {
        private SQLiteConnection koneksi;

        public Data_Driver()
        {
            
            koneksi = new SQLiteConnection("Data Source=D:\\22.11.4696\\RentalCar\\Data\\Rental.db;Version=3;");
            InitializeComponent();
            tampil_data();
        }

        private void tampil_data()
        {
            koneksi.Open();
            SQLiteCommand cmd = koneksi.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM DataDriver";
            cmd.ExecuteNonQuery();
            DataTable dta = new DataTable();
            SQLiteDataAdapter dataadp = new SQLiteDataAdapter(cmd);
            dataadp.Fill(dta);
            dataGridView1.DataSource = dta;
            koneksi.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            koneksi.Open();
            SQLiteCommand cmd = koneksi.CreateCommand();
            cmd.CommandText = "INSERT INTO DataDriver (ID, Driver, Alamat, NoHP) VALUES (@id, @driver, @alamat, @nohp)";
            cmd.Parameters.AddWithValue("@id", textBox1.Text);
            cmd.Parameters.AddWithValue("@driver", textBox2.Text);
            cmd.Parameters.AddWithValue("@alamat", textBox3.Text);
            cmd.Parameters.AddWithValue("@nohp", textBox4.Text);
            cmd.ExecuteNonQuery();
            koneksi.Close();
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            tampil_data();
            MessageBox.Show("Data telah ditambahkan");
            textBox1.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            koneksi.Open();
            SQLiteCommand cmd = koneksi.CreateCommand();
            cmd.CommandText = "UPDATE DataDriver SET ID = @id, NoHp = @nohp, Alamat = @alamat WHERE Driver = @driver";
            cmd.Parameters.AddWithValue("@id", textBox1.Text);
            cmd.Parameters.AddWithValue("@driver", textBox2.Text);
            cmd.Parameters.AddWithValue("@alamat", textBox3.Text);
            cmd.Parameters.AddWithValue("@nohp", textBox4.Text);
            cmd.ExecuteNonQuery();
            koneksi.Close();

            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();

            tampil_data();

            MessageBox.Show("Data telah diubah");

            textBox1.Focus();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            koneksi.Open();
            SQLiteCommand cmd = koneksi.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "DELETE FROM DataDriver WHERE ID = @id";
            cmd.Parameters.AddWithValue("@id", textBox1.Text);
            cmd.ExecuteNonQuery();
            koneksi.Close();
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            tampil_data();
            MessageBox.Show("Data berhasil dihapus");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox1.Focus();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Home panggil = new Home();
            panggil.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            tampil_data();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            // Membuka koneksi ke database
            koneksi.Open();
            SQLiteCommand cmd_check = koneksi.CreateCommand();
            cmd_check.CommandText = "SELECT COUNT(*) FROM DataDriver WHERE ID = @id";
            cmd_check.Parameters.AddWithValue("@id", textBox1.Text);
            int count = Convert.ToInt32(cmd_check.ExecuteScalar());

            if (count == 0)
            {
                MessageBox.Show("ID tidak ditemukan");
            }
            else
            {
                SQLiteCommand cmd = koneksi.CreateCommand();
                cmd.CommandText = "SELECT * FROM DataDriver WHERE ID = @id";
                cmd.Parameters.AddWithValue("@id", textBox1.Text);

                SQLiteDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    textBox1.Text = reader["ID"].ToString();
                    textBox2.Text = reader["Driver"].ToString();
                    textBox3.Text = reader["Alamat"].ToString();
                    textBox4.Text = reader["NoHp"].ToString();
                }
                reader.Close();
            }
            koneksi.Close();
        }
    }
    
}
