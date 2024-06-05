using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace otomasyon31
{
    public partial class adminpaneli : Form
    {
        public adminpaneli()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SoruEklemeFormu frm = new SoruEklemeFormu();
            frm.Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SınavTarihiEkle frm = new SınavTarihiEkle();
            frm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 geri = new Form1();
            geri.Show();
            this.Close();
        }
    }
}
