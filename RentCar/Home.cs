using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RentCar
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        private void Home_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Data_Mobil panggil = new Data_Mobil();
            panggil.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Data_Pelanggan panggil = new Data_Pelanggan();
            panggil.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Data_Sewa panggil = new Data_Sewa();
            panggil.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Data_Driver panggil = new Data_Driver();
            panggil.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login panggil = new Login();
            panggil.Show();
        }
    }
}
