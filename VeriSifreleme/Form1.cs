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

namespace VeriSifreleme
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		SqlConnection baglanti = new SqlConnection(@"Data Source=emin\SQLEXPRESS;Initial Catalog=VeriSifrelemeDb;Integrated Security=True;Encrypt=False");

		void listele()
		{
			SqlDataAdapter da = new SqlDataAdapter("Select * From TBLVERILER", baglanti);
			DataTable dt = new DataTable();
			da.Fill(dt);
			dataGridView1.DataSource = dt;
		}
		private void button1_Click(object sender, EventArgs e)
		{
			string ad = TxtAd.Text;
			byte[] adDizi = ASCIIEncoding.ASCII.GetBytes(ad);
			string adSifre = Convert.ToBase64String(adDizi);
			
			string soyad = TxtSoyad.Text;
			byte[] soyadDizi = ASCIIEncoding.ASCII.GetBytes(soyad);
			string soyadSifre = Convert.ToBase64String(soyadDizi);

			string mail = TxtMail.Text;
			byte[] mailDizi = ASCIIEncoding.ASCII.GetBytes(mail);
			string mailSifre = Convert.ToBase64String(mailDizi);

			string sifre = TxtSifre.Text;
			byte[] sifreDizi = ASCIIEncoding.ASCII.GetBytes(sifre);
			string sifreSifre = Convert.ToBase64String(sifreDizi);

			string hesapNo = TxtHesapNo.Text;
			byte[] hesapNoDizi = ASCIIEncoding.ASCII.GetBytes(hesapNo);
			string HesapNoSifre = Convert.ToBase64String(hesapNoDizi);

			baglanti.Open();
			SqlCommand komut = new SqlCommand("insert into TBLVERILER (AD,SOYAD,MAIL,SIFRE,HESAPNO) values (@p1,@p2,@p3,@p4,@p5)", baglanti);
			komut.Parameters.AddWithValue("@p1", adSifre);
			komut.Parameters.AddWithValue("@p2", soyadSifre);
			komut.Parameters.AddWithValue("@p3", mailSifre);
			komut.Parameters.AddWithValue("@p4", sifreSifre);
			komut.Parameters.AddWithValue("@p5", HesapNoSifre);
			komut.ExecuteNonQuery();
			baglanti.Close();
			MessageBox.Show("Veriler eklendi");
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			listele();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			string adCozum = TxtAd.Text;
			byte[] adCozumDizi = Convert.FromBase64String(adCozum);
			string adVerisi = ASCIIEncoding.ASCII.GetString(adCozumDizi);
			label6.Text = adVerisi;
		}
	}
}
