
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
    public partial class Data_Pelanggan : Form
    {
        private SQLiteConnection koneksi;

        public Data_Pelanggan()
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
            cmd.CommandText = "select * from [DataPelanggan]";
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
            cmd.CommandText = "INSERT INTO DataPelanggan (IDmember, NIK, Nama, Alamat, NoHP) VALUES (@idmember, @nik, @nama, @alamat, @nohp)";
            cmd.Parameters.AddWithValue("@idmember", textBox1.Text);
            cmd.Parameters.AddWithValue("@nik", textBox2.Text);
            cmd.Parameters.AddWithValue("@nama", textBox3.Text);
            cmd.Parameters.AddWithValue("@alamat", textBox4.Text);
            cmd.Parameters.AddWithValue("@nohp", textBox5.Text);
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

        private void button2_Click(object sender, EventArgs e)
        {
            koneksi.Open();

            SQLiteCommand cmd = koneksi.CreateCommand();
            cmd.CommandText = "update DataPelanggan set NIK = @nik, Nama = @nama, NoHP = @nohp, Alamat = @alamat where IDmember = @idmember";
            cmd.Parameters.AddWithValue("idmember", textBox1.Text);
            cmd.Parameters.AddWithValue("nik", textBox2.Text);
            cmd.Parameters.AddWithValue("nama", textBox3.Text);
            cmd.Parameters.AddWithValue("alamat", textBox4.Text);
            cmd.Parameters.AddWithValue("nohp", textBox5.Text);

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

        private void button3_Click(object sender, EventArgs e)
        {
            koneksi.Open();
            SQLiteCommand cmd = koneksi.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "delete from [DataPelanggan] where IDmember = '" + textBox1.Text + "'";
            cmd.ExecuteNonQuery();
            koneksi.Close();
            textBox1.Clear();
            tampil_data();
            MessageBox.Show("Data berhasil dihapus");
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

        private void button6_Click(object sender, EventArgs e)
        {
            tampil_data();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            // Membuka koneksi ke database
            koneksi.Open();
            SQLiteCommand cmd_check = koneksi.CreateCommand();
            cmd_check.CommandText = "SELECT COUNT(*) FROM DataPelanggan WHERE IDmember LIKE @idmember";
            cmd_check.Parameters.AddWithValue("@idmember", "%" + textBox1.Text + "%");
            int count = Convert.ToInt32(cmd_check.ExecuteScalar());

            if (count > 0)
            {
                SQLiteCommand cmd = koneksi.CreateCommand();
                cmd.CommandText = "SELECT * FROM DataPelanggan WHERE IDmember LIKE @idmember";
                cmd.Parameters.AddWithValue("@idmember", "%" + textBox1.Text + "%");
                SQLiteDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    textBox2.Text = reader["NIK"].ToString();
                    textBox3.Text = reader["Nama"].ToString();
                    textBox4.Text = reader["Alamat"].ToString();
                    textBox5.Text = reader["NoHP"].ToString();
                }

                reader.Close();
            }
            else
            {
                MessageBox.Show("Nama pelanggan tidak ditemukan");
            }

            koneksi.Close();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
    }

