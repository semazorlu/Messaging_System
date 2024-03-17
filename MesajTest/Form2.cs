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

namespace MesajTest
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        public string numara;

        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-JDK13S7;Initial Catalog=Test;Integrated Security=True");

        void gelenKutusu()
        {
            SqlDataAdapter da1 = new SqlDataAdapter("Select * From Tblmesajlar WHERE alıcı=" + numara, baglanti);  // numara değişkenini alıcının karşısına otomatik olarak eşitler
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);  //da1'i dt1'den gelen değerle doldurur
            dataGridView1.DataSource = dt1;  // dataGridView1'in veri kaynağı olarak dt1 gösterilir
        }

        void gidenKutusu()
        {
            SqlDataAdapter da2 = new SqlDataAdapter("Select * From Tblmesajlar WHERE gonderen=" + numara, baglanti);  // numara değişkenini alıcının karşısına otomatik olarak eşitler
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);  //da2'i dt2'den gelen değerle doldurur
            dataGridView2.DataSource = dt2;  // dataGridView2'in veri kaynağı olarak dt2 gösterilir
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            LblNumara.Text = numara;
            gelenKutusu();
            gidenKutusu();

            // Ad Soyadı Çekme
            baglanti.Open();

            SqlCommand komut = new SqlCommand("Select Ad,Soyad from TBLKISILER where numara=" + numara, baglanti);
            SqlDataReader dr = komut.ExecuteReader();

            while (dr.Read())
            {
                LblAdSoyad.Text = dr[0] + " " + dr[1];
            }

            baglanti.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();

            SqlCommand komut = new SqlCommand("insert into TBLMESAJLAR (gonderen, alıcı, baslık, ıcerık) values (@p1, @p2, @p3, @p4)", baglanti);
            komut.Parameters.AddWithValue("@p1", numara);
            komut.Parameters.AddWithValue("@p2", maskedTextBox1.Text);
            komut.Parameters.AddWithValue("@p3", textBox1.Text);
            komut.Parameters.AddWithValue("@p4", richTextBox1.Text);
            komut.ExecuteNonQuery();  // İşlemi gerçekleştir anlamında

            baglanti.Close();

            MessageBox.Show("Mesajınız iletildi !");
            gidenKutusu();
        }
    }
}
