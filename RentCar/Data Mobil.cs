
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
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace RentCar
{
    public partial class Data_Mobil : Form
    {
        private SQLiteConnection koneksi;
        public Data_Mobil()
        {
            koneksi = new SQLiteConnection("Data Source=D:\\22.11.4696\\RentalCar\\Data\\Rental.db;Version=3;");
            InitializeComponent();
            tampil_data();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            koneksi.Open();
            SQLiteCommand cmd = koneksi.CreateCommand();
            cmd.CommandText = "INSERT INTO DataMobil (IDmobil, Nopol, Merk, Mobil, Tahun) VALUES (@idmobil, @nopol, @merk, @mobil, @tahun)";
            cmd.Parameters.AddWithValue("@idmobil", textBox1.Text);
            cmd.Parameters.AddWithValue("@nopol", textBox2.Text);
            cmd.Parameters.AddWithValue("@merk", textBox3.Text);
            cmd.Parameters.AddWithValue("@mobil", textBox4.Text);
            cmd.Parameters.AddWithValue("@tahun", textBox5.Text);
            cmd.ExecuteNonQuery();
            koneksi.Close();
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            tampil_data();
            MessageBox.Show("Data telah ditambahkan");
            textBox1.Focus();
            
        }

        private void tampil_data()
        {
            koneksi.Open();
            SQLiteCommand cmd = koneksi.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM DataMobil";
            cmd.ExecuteNonQuery();
            DataTable dta = new DataTable();
            SQLiteDataAdapter dataadp = new SQLiteDataAdapter(cmd);
            dataadp.Fill(dta);
            dataGridView1.DataSource = dta;
            koneksi.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            tampil_data();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            koneksi.Open();
            SQLiteCommand cmd = koneksi.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "DELETE FROM DataMobil WHERE IDmobil = @idmobil";
            cmd.Parameters.AddWithValue("@idmobil", textBox1.Text);
            cmd.ExecuteNonQuery();
            koneksi.Close();
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            tampil_data();
            MessageBox.Show("Data berhasil dihapus");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            koneksi.Open();
            SQLiteCommand cmd = koneksi.CreateCommand();
            cmd.CommandText = "UPDATE DataMobil SET Nopol = @nopol, Merk = @merk, Mobil = @mobil, Tahun = @tahun WHERE IDmobil = @idmobil";
            cmd.Parameters.AddWithValue("@idmobil", textBox1.Text);
            cmd.Parameters.AddWithValue("@nopol", textBox2.Text);
            cmd.Parameters.AddWithValue("@merk", textBox3.Text);
            cmd.Parameters.AddWithValue("@mobil", textBox4.Text);
            cmd.Parameters.AddWithValue("@tahun", textBox5.Text);
            cmd.ExecuteNonQuery();
            koneksi.Close();

            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();

            tampil_data();

            MessageBox.Show("Data telah diubah");

            textBox1.Focus();
        }

            private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox1.Focus();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Home panggil = new Home();
            panggil.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            // Membuka koneksi ke database
            koneksi.Open();
            SQLiteCommand cmd_check = koneksi.CreateCommand();
            cmd_check.CommandText = "SELECT COUNT(*) FROM DataMobil WHERE IDmobil = @idmobil";
            cmd_check.Parameters.AddWithValue("@idmobil", textBox1.Text);
            int count = Convert.ToInt32(cmd_check.ExecuteScalar());

            if (count == 0)
            {
                MessageBox.Show("ID Mobil tidak ditemukan");
            }
            else
            {
                SQLiteCommand cmd = koneksi.CreateCommand();
                cmd.CommandText = "SELECT * FROM DataMobil WHERE IDmobil = @idmobil";
                cmd.Parameters.AddWithValue("@idmobil", textBox1.Text);

                SQLiteDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    textBox1.Text = reader["IDmobil"].ToString();
                    textBox2.Text = reader["Nopol"].ToString();
                    textBox3.Text = reader["Merk"].ToString();
                    textBox4.Text = reader["Mobil"].ToString();
                    textBox5.Text = reader["Tahun"].ToString();
                }
                reader.Close();
            }
            koneksi.Close();
        }
    }
}
