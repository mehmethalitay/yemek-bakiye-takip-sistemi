using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;

namespace Yemek_Takip
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        SQLiteConnection con;
        SQLiteDataAdapter da;
        SQLiteCommand cmd;
        DataSet ds;

        void listele()
        {
            con = new SQLiteConnection("Data Source=yemektakip.sqlite;Version=3;");
            da = new SQLiteDataAdapter("Select *From musteriler", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "musteriler");
            con.Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                listele();
                cmd = new SQLiteCommand();
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "insert into musteriler(musterikodu,ad,soyad,firma,telefon,adres,bakiye) values ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + textBox6.Text + "','" + textBox7.Text + "')";
                cmd.ExecuteNonQuery();
                con.Close();
                
                try
                {
                    string tarih;
                    DateTime dt = DateTime.Now;

                    tarih = DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss");

                    con.Open();
                    cmd.CommandText = "insert into rapor(tarih,ad,soyad,firma,dusen,eklenen,mkodu) values ('" + tarih + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + "0" + "','" + textBox7.Text + "','" + textBox1.Text + "')";
                    cmd.ExecuteNonQuery();
                    con.Close();

                }
                catch (Exception)
                {

                    MessageBox.Show("İşlem hatası firmayı arayın !");
                }
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
                textBox6.Clear();
                textBox7.Clear();
                MessageBox.Show("Kayıt Başarılı");
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.ToString());
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form1 f1 = (Form1)Application.OpenForms["Form1"];
            f1.listele();
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
