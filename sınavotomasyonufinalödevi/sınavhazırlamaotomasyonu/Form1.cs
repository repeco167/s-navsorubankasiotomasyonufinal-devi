using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;   //  formu sqle bağlamak için bu kodu yazdım
using System.Data.SqlClient;// 
using otomasyon31.sınavotomasyonuDataSetTableAdapters;
using System.Diagnostics.Eventing.Reader;

namespace otomasyon31
{
    public partial class Form1 : Form

    {
       

        public Form1()
           
            
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-QPC87EO;Initial Catalog=sınavotomasyonu;Integrated Security=True;Encrypt=False"); //burada sqlden bağlantıyı alıp formu sqle bağladım

        private void textBox1_Click(object sender, EventArgs e)
        {
            panel3.BackColor  = Color.White;
            panel4.BackColor = SystemColors.Control;// buradaki komutlarla da textbozlar arası geçiş yaparken birine bastığında diğerini daha sönük göstermesi için bu kodu yazdım
        }

        private void textBox3_Click(object sender, EventArgs e)
        {
            panel3.BackColor = SystemColors.Control;
            panel3.BackColor = Color.White;
        }
       
        private void Btn_Admin_Click(object sender, EventArgs e)
        {
        baglanti.Open(); // burada bağlantıyı açtım
            SqlCommand komut = new SqlCommand("select * from Tbl_admin where Kullanici_adi ='" + textBox1.Text + "' and Sifre='" + textBox3.Text + "'", baglanti);//burada textboxları sqldeli değerlere bağladım
            SqlDataReader dr = komut.ExecuteReader();// bu komutla sql komutunu çalıştırdım
            if (dr.Read())
            {
                MessageBox.Show("Tebrikler Hoşgeldiniz Admin Giriş Paneli");// eğer şifre doğruysa girdi
                
                adminpaneli frm = new adminpaneli(); // buraya yeni bir method tanımladım 
                frm.Show();// oluşturduğum methodla show komutunu kullanarak admin paneli formunu açıcak eğer şifre doğruysa
                textBox1.Clear();// ve textbox1 ve 3 ü temizlettirrdim
                textBox3.Clear();

            }
            
            
            else 
            {
                MessageBox.Show("Tekrar deneyiniz Ya da Admin değilsiniz");// şifre yanlış olduğunda da bizi uyardı 
            
            
            }
            baglanti.Close(); // bağlantı close diyerek de bağlantıyı kapattım

            
           
                    

            

            
                

            


        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from Tbl_Ogrenci where Okullanici_adi ='" + textBox1.Text + "' and Osifre='" + textBox3.Text + "'", baglanti);
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                MessageBox.Show("Tebrikler Hoşgeldiniz Öğrenci Giriş Paneli");

                öğrencipaneli frm = new öğrencipaneli();
                frm.Show();
                textBox1.Clear();
                textBox3.Clear();
            }
            else
            {
                MessageBox.Show("Tekrar deneyiniz");


            }
            baglanti.Close();
        }
    }
}
