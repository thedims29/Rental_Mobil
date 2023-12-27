
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace RentCar
{
    public partial class Data_Sewa : Form
    {
        private SQLiteConnection koneksi;

        public Data_Sewa()
        {
            koneksi = new SQLiteConnection("Data Source=D:\\22.11.4696\\RentalCar\\Data\\Rental.db;Version=3;");
            InitializeComponent();
            LoadDataPelanggan();
            LoadDataMobil();
            LoadDataDriver();
            tampil_data();


        }

        private void Data_Sewa_Load(object sender, EventArgs e)
        {
            LoadDataPelanggan();
            LoadDataMobil();
            LoadDataDriver();
        }

        private void LoadDataPelanggan()
        {
            
            comboBox1.DisplayMember = "Nama";
            comboBox1.ValueMember = "Nama";
            
            try
            {
                koneksi.Open();
                SQLiteDataAdapter dataadp = new SQLiteDataAdapter("select [Nama] from [DataPelanggan]", koneksi);
                DataTable dta = new DataTable();
                dataadp.Fill(dta);

                comboBox1.DataSource = dta;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan: " + ex.Message);
            }
            finally
            {
                koneksi.Close();
            }
        }

        private void LoadDataMobil()
        {
            comboBox2.DisplayMember = "Mobil";
            comboBox2.ValueMember = "Mobil";

            try
            {
                koneksi.Open();
                SQLiteDataAdapter dataadp = new SQLiteDataAdapter("select [Mobil] from [DataMobil]", koneksi);
                DataTable dta = new DataTable();
                dataadp.Fill(dta);

                comboBox2.DataSource = dta;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan: " + ex.Message);
            }
            finally
            {
                koneksi.Close();
            }
        }

        private void LoadDataDriver()
        {
            comboBox3.DisplayMember = "Driver";
            comboBox3.ValueMember = "Driver";

            try
            {
                koneksi.Open();
                SQLiteDataAdapter dataadp = new SQLiteDataAdapter("select [Driver] from [DataDriver]", koneksi);
                DataTable dta = new DataTable();
                dataadp.Fill(dta);

                comboBox3.DataSource = dta;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan: " + ex.Message);
            }
            finally
            {
                koneksi.Close();
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void tampil_data()
        {

            koneksi.Open();
            SQLiteCommand cmd = koneksi.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from [DataSewa]";
            cmd.ExecuteNonQuery();
            DataTable dta = new DataTable();
            SQLiteDataAdapter dataadp = new SQLiteDataAdapter(cmd);
            dataadp.Fill(dta);
            dataGridView1.DataSource = dta;
            koneksi.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
 
            try
            {
                koneksi.Open();
                SQLiteCommand cmd = koneksi.CreateCommand();
                cmd.CommandText = "insert into DataSewa (NoSewa, Nama, Mobil, Driver, Tanggal) values (@nosewa, @nama, @mobil, @driver, @tanggal)";
                cmd.Parameters.AddWithValue("nosewa", textBox1.Text);
                cmd.Parameters.AddWithValue("nama", comboBox1.Text);
                cmd.Parameters.AddWithValue("mobil", comboBox2.Text);
                cmd.Parameters.AddWithValue("driver", comboBox3.Text);
                cmd.Parameters.AddWithValue("tanggal", dateTimePicker1.Value);
                cmd.ExecuteNonQuery();

                textBox1.Clear();
                comboBox1.SelectedIndex = -1;
                comboBox2.SelectedIndex = -1;
                comboBox3.SelectedIndex = -1;
                dateTimePicker1.Value = DateTime.Now;
                textBox1.Focus();
                

                MessageBox.Show("Data telah ditambahkan");
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Terjadi kesalahan: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan: " + ex.Message);
            }
            finally
            {
                koneksi.Close();
                tampil_data();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(comboBox1.Text) || string.IsNullOrEmpty(comboBox2.Text) || string.IsNullOrEmpty(comboBox3.Text))
            {
                MessageBox.Show("Harap isi semua field");
                return;
            } 

            try
            {
                koneksi.Open();
                SQLiteCommand cmd = koneksi.CreateCommand();
                cmd.CommandText = "update DataSewa set Nama = @nama, Mobil = @mobil, Driver = @driver, Tanggal = @tanggal where NoSewa = @nosewa";
                cmd.Parameters.AddWithValue("nosewa", textBox1.Text);
                cmd.Parameters.AddWithValue("nama", comboBox1.Text);
                cmd.Parameters.AddWithValue("mobil", comboBox2.Text);
                cmd.Parameters.AddWithValue("driver", comboBox3.Text);
                cmd.Parameters.AddWithValue("tanggal", dateTimePicker1.Value);
                cmd.ExecuteNonQuery();

                textBox1.Clear();
                comboBox1.SelectedIndex = -1;
                comboBox2.SelectedIndex = -1;
                comboBox3.SelectedIndex = -1;
                dateTimePicker1.Value = DateTime.Now;
                textBox1.Focus();
                

                MessageBox.Show("Data telah diperbarui");
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Terjadi kesalahan: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan: " + ex.Message);
            }
            finally
            {
                koneksi.Close();
                tampil_data();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            /*if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Harap isi NoSewa");
                return;
            }*/

            try
            {
                koneksi.Open();
                SQLiteCommand cmd = koneksi.CreateCommand();
                cmd.CommandText = "delete from DataSewa where NoSewa = @nosewa";
                cmd.Parameters.AddWithValue("nosewa", textBox1.Text);
                cmd.ExecuteNonQuery();

                textBox1.Clear();
                comboBox1.SelectedIndex = -1;
                comboBox2.SelectedIndex = -1;
                comboBox3.SelectedIndex = -1;
                dateTimePicker1.Value = DateTime.Now;
                textBox1.Focus();
                

                MessageBox.Show("Data telah dihapus");
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Terjadi kesalahan: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan: " + ex.Message);
            }
            finally
            {
                koneksi.Close();
                tampil_data();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
            dateTimePicker1.Value = DateTime.Now;
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

        private void button7_Click(object sender, EventArgs e)
        {
            // Membuka koneksi ke database
            koneksi.Open();
            SQLiteCommand cmd_check = koneksi.CreateCommand();
            cmd_check.CommandText = "SELECT COUNT(*) FROM DataSewa WHERE NoSewa = @nosewa";
            cmd_check.Parameters.AddWithValue("@nosewa", textBox1.Text);
            int count = Convert.ToInt32(cmd_check.ExecuteScalar());

            if (count == 0)
            {
                MessageBox.Show("ID tidak ditemukan");
            }
            else
            {
                SQLiteCommand cmd = koneksi.CreateCommand();
                cmd.CommandText = "SELECT * FROM DataSewa WHERE NoSewa = @nosewa";
                cmd.Parameters.AddWithValue("@nosewa", textBox1.Text);

                SQLiteDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    textBox1.Text = reader["NoSewa"].ToString();
                    comboBox1.Text= reader["Nama"].ToString();
                    comboBox2.Text = reader["Mobil"].ToString();
                    comboBox3.Text = reader["Driver"].ToString();
                    dateTimePicker1.Value = Convert.ToDateTime(reader["Tanggal"]);
                }
                reader.Close();
            }
            koneksi.Close();
        }
    }
    
}
