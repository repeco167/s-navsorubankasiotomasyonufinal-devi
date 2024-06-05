using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using ClosedXML.Excel;

// Proje için kullanılan namespace
namespace otomasyon31
{
    // Soru ekleme formu için sınıf tanımı
    public partial class SoruEklemeFormu : Form
    {
        // Veritabanı bağlantı dizesi
        static string constring = "Data Source=DESKTOP-QPC87EO;Initial Catalog=sınavotomasyonu;Integrated Security=True;Encrypt=False";
        SqlConnection baglan = new SqlConnection(constring);

        // Form başlatıldığında çağırılan yapılandırıcı
        public SoruEklemeFormu()
        {
            InitializeComponent();
        }

        // Veritabanından kayıtları çeken metot
        public void kayitlarigetir()
        {
            if (baglan.State == ConnectionState.Closed)
            {
                baglan.Open();
            }
            string getir = "Select * from Tbl_SoruAciklama1";
            SqlCommand komut = new SqlCommand(getir, baglan);
            SqlDataAdapter ad = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            dataGridView1.DataSource = dt;
            baglan.Close();
        }

        // Soru kaydetme işlemini gerçekleştiren butonun tıklandığında çalışan metot
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (baglan.State == ConnectionState.Closed)
                {
                    baglan.Open();
                }
                string kayit = "insert into Tbl_SoruAciklama1 (ID, Soru_Aciklama, a, b, c, d, DoğruCevap) values (@ID, @Soru_Aciklama, @a, @b, @c, @d, @DoğruCevap)";
                SqlCommand komut = new SqlCommand(kayit, baglan);
                komut.Parameters.AddWithValue("@ID", textBox5.Text);
                komut.Parameters.AddWithValue("@Soru_Aciklama", richTextBox1.Text);
                komut.Parameters.AddWithValue("@a", textBox1.Text);
                komut.Parameters.AddWithValue("@b", textBox2.Text);
                komut.Parameters.AddWithValue("@c", textBox3.Text);
                komut.Parameters.AddWithValue("@d", textBox4.Text);
                komut.Parameters.AddWithValue("@DoğruCevap", comboBox1.Text);

                komut.ExecuteNonQuery();
                MessageBox.Show("Soru Başarıyla Eklendi");
            }
            catch (Exception hata)
            {
                MessageBox.Show("Bir hata var: " + hata.Message);
            }
            finally
            {
                if (baglan.State == ConnectionState.Open)
                {
                    baglan.Close();
                }
            }
        }

        // Kayıtları yenilemek için kullanılan buton
        private void button2_Click(object sender, EventArgs e)
        {
            kayitlarigetir();
        }

        // Veri silme işlemi
        public void verisil(int id)
        {
            if (baglan.State == ConnectionState.Closed)
            {
                baglan.Open();
            }
            string sil = "Delete From Tbl_SoruAciklama1 Where ID = @id";
            SqlCommand komut = new SqlCommand(sil, baglan);
            komut.Parameters.AddWithValue("@id", id);
            komut.ExecuteNonQuery();
            baglan.Close();
        }

        // Seçilen soruyu silmek için kullanılan buton
        private void Btn_Sil_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow drow in dataGridView1.SelectedRows)
            {
                int id = Convert.ToInt32(drow.Cells[0].Value);
                verisil(id);
                kayitlarigetir();
                MessageBox.Show("Başarıyla silindi");
            }
        }

        // Tüm metin kutularını temizleyen buton
        private void button5_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            richTextBox1.Focus();
        }

        // Rastgele soru seçimini yaparak listelemek için kullanılan buton
        private void button4_Click(object sender, EventArgs e)
        {
            int soruSayisi;
            if (int.TryParse(textBox6.Text, out soruSayisi) && dataGridView1.DataSource != null)
            {
                DataTable currentData = ((DataTable)dataGridView1.DataSource).Copy();
                if (soruSayisi > currentData.Rows.Count)
                {
                    MessageBox.Show("Girilen sayı mevcut soru sayısından fazla. Lütfen daha küçük bir sayı giriniz.");
                }
                else
                {
                    DataTable randomData = GetRandomData(currentData, soruSayisi);
                    dataGridView1.DataSource = randomData;
                }
            }
            else
            {
                MessageBox.Show("Lütfen geçerli bir sayı giriniz ve verilerin yüklendiğinden emin olun.");
            }
        }

        // Rastgele soru seçimi yaparak yeni bir DataTable oluşturan metot
        private DataTable GetRandomData(DataTable currentData, int count)
        {
            Random rand = new Random();
            DataTable result = currentData.Clone();

            List<int> selectedIndexes = new List<int>();
            while (selectedIndexes.Count < count)
            {
                int index = rand.Next(currentData.Rows.Count);
                if (!selectedIndexes.Contains(index))
                {
                    selectedIndexes.Add(index);
                    result.ImportRow(currentData.Rows[index]);
                }
            }

            return result;
        }

        // Excel dosyası olarak kaydetmek için kullanılan buton
        private void button6_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.DataSource == null)
            {
                MessageBox.Show("Önce verileri karıştırın ve listeleyin.");
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog { Filter = "Excel Documents (.xlsx)|.xlsx", FileName = "sorular.xlsx" })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("Karıştırılmış Sorular");
                        var data = dataGridView1.DataSource as DataTable;
                        worksheet.Cell(1, 1).InsertTable(data, "Sorular", true);
                        workbook.SaveAs(sfd.FileName);
                    }

                    MessageBox.Show("Excel dosyası başarıyla kaydedildi: " + sfd.FileName);
                }
            }
        }
        int i = 0;
        private void button3_Click(object sender, EventArgs e)
        {
            baglan.Open();
            string kayitguncelle = ("Update Tbl_SoruAciklama1 Set ID=@ıd, Soru_Aciklama = @soruacik, a = @a, b =@b ,c=@c, d =@d, DoğruCevap=@dogrucvp");
            SqlCommand komut = new SqlCommand(kayitguncelle, baglan);
            komut.Parameters.AddWithValue("@soruacik", richTextBox1.Text);
            komut.Parameters.AddWithValue("@ID", dataGridView1.Rows[i].Cells[0].Value);
           
            komut.Parameters.AddWithValue("@a",textBox1.Text);
            komut.Parameters.AddWithValue("@b",textBox2.Text);
            komut.Parameters.AddWithValue("@c",textBox3.Text);
            komut.Parameters.AddWithValue("@d",textBox4.Text);
            komut.Parameters.AddWithValue("@dogrucvp",comboBox1.Text);
            komut.ExecuteNonQuery();
            MessageBox.Show("kayıtlar başarıyla güncellendi");
            baglan.Close();
            kayitlarigetir();

        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            i = e.RowIndex;
            richTextBox1.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
            
           textBox1.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
            textBox2.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();
            textBox3.Text = dataGridView1.Rows[i].Cells[4].Value.ToString();
            textBox4.Text = dataGridView1.Rows[i].Cells[5].Value.ToString();
            comboBox1.Text = dataGridView1.Rows[i].Cells[6].Value.ToString();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            adminpaneli geri = new adminpaneli();
            geri.Show();
            this.Close();
        }
    }
}