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
using System.IO;

namespace Yemek_Takip
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        SQLiteConnection con;
        SQLiteDataAdapter da;
        SQLiteCommand cmd;
        DataSet ds;
        int dusbak, subak, kalanbak;


        public void listele()
        {
            con = new SQLiteConnection("Data Source=yemektakip.sqlite;Version=3;");
            da = new SQLiteDataAdapter("Select *From musteriler", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "musteriler");
            dataGridView1.DataSource = ds.Tables["musteriler"];
            con.Close();
        }


        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            listele();
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderText = "Müşteri Kodu";
            dataGridView1.Columns[2].HeaderText = "Ad";
            dataGridView1.Columns[3].HeaderText = "Soyad";
            dataGridView1.Columns[4].HeaderText = "Firma";
            dataGridView1.Columns[5].HeaderText = "Telefon";
            dataGridView1.Columns[6].HeaderText = "Adres";
            dataGridView1.Columns[7].HeaderText = "Bakiye";
            listele();

            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox2.ReadOnly = true;
            textBox3.ReadOnly = true;
            textBox4.ReadOnly = true;
            textBox5.ReadOnly = true;
            textBox6.ReadOnly = true;
            textBox7.ReadOnly = true;
            button2.Enabled = false;
            button5.Enabled = false;
            button1.Enabled = true;

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (radioButton1.Checked == false)
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }

            else
            {
                e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsSeparator(e.KeyChar);
            }
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            linkLabel1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            linkLabel2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            textBox6.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            textBox7.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialogResult = MessageBox.Show("Kişiyi güncellemek istiyormusunuz ?", "Kişi Güncelle", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    cmd = new SQLiteCommand();
                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = "update musteriler set musterikodu ='" + linkLabel2.Text + "',ad ='" + textBox2.Text + "', soyad ='" + textBox3.Text + "', firma ='" + textBox4.Text + "', telefon ='" + textBox5.Text + "', adres='" + textBox6.Text + "', bakiye='" + textBox7.Text + "' where ID=" + linkLabel1.Text + "";
                    cmd.ExecuteNonQuery();
                    con.Close();

                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    textBox4.Clear();
                    textBox5.Clear();
                    textBox6.Clear();
                    textBox7.Clear();

                    textBox2.ReadOnly = true;
                    textBox3.ReadOnly = true;
                    textBox4.ReadOnly = true;
                    textBox5.ReadOnly = true;
                    textBox6.ReadOnly = true;
                    textBox7.ReadOnly = true;
                    button2.Enabled = false;
                    button5.Enabled = false;
                    button1.Enabled = true;


                    listele();
                    MessageBox.Show("Günceleme Başarılı");

                }
            }
            catch (Exception)
            {
                MessageBox.Show("İşlencek veri yok yada işlem hatası !");
            }
        }
                
                
                    
          
        

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string aranan = textBox1.Text.Trim().ToUpper();
                if (radioButton1.Checked == false)
                {

                    try
                    {
                        da = new SQLiteDataAdapter("select * from musteriler where musterikodu like '" + aranan + "%'", con);
                        ds = new DataSet();
                        da.Fill(ds, "musteriler");
                        dataGridView1.DataSource = ds.Tables["musteriler"];
                    }
                    catch (Exception hata)
                    {

                        MessageBox.Show(hata.ToString());
                    }
                }

                else
                {
                    try
                    {
                        da = new SQLiteDataAdapter("select * from musteriler where ad like '" + aranan + "%'", con);
                        ds = new DataSet();
                        da.Fill(ds, "musteriler");
                        dataGridView1.DataSource = ds.Tables["musteriler"];
                    }
                    catch (Exception hata)
                    {

                        MessageBox.Show(hata.ToString());
                    }
                }
            }
            catch (Exception hata)
            {

                MessageBox.Show(hata.ToString());
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialogResult = MessageBox.Show("Kişiyi silmek istiyormusunuz ?", "Kişi Sil", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {

                    cmd = new SQLiteCommand();
                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = "delete from musteriler where id=" + linkLabel1.Text + "";
                    cmd.ExecuteNonQuery();
                    con.Close();
                    listele();

                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    textBox4.Clear();
                    textBox5.Clear();
                    textBox6.Clear();
                    textBox7.Clear();

                    textBox2.ReadOnly = true;
                    textBox3.ReadOnly = true;
                    textBox4.ReadOnly = true;
                    textBox5.ReadOnly = true;
                    textBox6.ReadOnly = true;
                    textBox7.ReadOnly = true;
                    button2.Enabled = false;
                    button5.Enabled = false;
                    button1.Enabled = true;

                    listele();
                }
                    
            }
            catch (Exception)
            {
                MessageBox.Show("İşlencek veri yok yada işlem hatası ! ");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialogResult = MessageBox.Show("Tutar eklemek istiyormusunuz ?", "Tutar Ekle", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    
                        subak = Convert.ToInt32(textBox7.Text);
                        dusbak = Convert.ToInt32(textBox8.Text);

                        kalanbak = subak + dusbak;
                        linkLabel3.Text = kalanbak.ToString();
                        if (kalanbak >= 0)
                        {
                            cmd = new SQLiteCommand();
                            con.Open();
                            cmd.Connection = con;
                            cmd.CommandText = "update musteriler set bakiye='" + linkLabel3.Text + "' where id=" + linkLabel1.Text + "";
                            cmd.ExecuteNonQuery();
                            con.Close();
                        try
                        {
                            string tarih;
                            DateTime dt = DateTime.Now;

                            tarih = DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss");

                            con.Open();
                            cmd.CommandText = "insert into rapor(tarih,ad,soyad,firma,dusen,eklenen,mkodu) values ('" + tarih + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + "0" + "','" + textBox8.Text + "','" + textBox1.Text + "')";
                            cmd.ExecuteNonQuery();
                            con.Close();

                        }
                        catch (Exception)
                        {

                            MessageBox.Show("İşlem hatası firmayı arayın !");
                        }

                        listele();
                            textBox1.Clear();
                            textBox2.Clear();
                            textBox3.Clear();
                            textBox4.Clear();
                            textBox5.Clear();
                            textBox6.Clear();
                            textBox7.Clear();
                            textBox8.Clear();
                            textBox8.Clear();
                        }
                        else
                        {
                            MessageBox.Show("İşlem Hatası");
                        }

                    }
            }
            catch (Exception)
            {
                MessageBox.Show("Tutar giriniz !");
            }
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
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
            textBox8.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox2.ReadOnly = false;
            textBox3.ReadOnly = false;
            textBox4.ReadOnly = false;
            textBox5.ReadOnly = false;
            textBox6.ReadOnly = false;
            textBox7.ReadOnly = false;
            button2.Enabled = true;
            button5.Enabled = true;
            button1.Enabled = false;
        }
    }
}
