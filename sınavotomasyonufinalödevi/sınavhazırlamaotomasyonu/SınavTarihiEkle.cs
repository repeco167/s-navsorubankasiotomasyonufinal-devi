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
    public partial class SınavTarihiEkle : Form
    {
        public SınavTarihiEkle()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            öğrencipaneli fr = new öğrencipaneli();
            fr.label4.Text = textBox1.Text;
            fr.label5.Text =dateTimePicker1.Text;
            fr.ShowDialog();
        }
    }
}
