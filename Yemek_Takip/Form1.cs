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
using Microsoft.VisualBasic;

namespace Yemek_Takip
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        SQLiteConnection con,con1;
        SQLiteDataAdapter da,da1;
        SQLiteCommand cmd;
        DataSet ds, ds1;
        int dusbak,subak,kalanbak;
        Form2 frm2 = new Form2();
        Form3 frm3 = new Form3();
        Form4 frm4 = new Form4();
        Form5 frm5 = new Form5();
        Form6 frm6 = new Form6();



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

        public void listele1()
        {
            con1 = new SQLiteConnection("Data Source=yemektakip.sqlite;Version=3;");
            da1 = new SQLiteDataAdapter("Select *From ayar", con);
            ds1 = new DataSet();
            con1.Open();
            da1.Fill(ds, "ayar");
            dataGridView2.DataSource = ds.Tables["ayar"];
            con1.Close();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            //if (File.Exists(@"C:\Windows\ybt.lic"))
            //{

            //}

            //else
            //{ 
            //    MessageBox.Show("Lisans bulunamadı veri güvenliği için lütfen Yetkili firma ile iletişime geçin !", "LİSANS HATASI", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    Application.Exit();
            //}

            linkLabel4.Text = "0";

            this.WindowState = FormWindowState.Normal;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Bounds = Screen.PrimaryScreen.Bounds;
            panel1.Location = new Point(
            this.ClientSize.Width / 2 - panel1.Size.Width / 2,
            this.ClientSize.Height / 2 - panel1.Size.Height / 2);
            panel1.Anchor = AnchorStyles.None;
            panel1.BackColor = Color.Transparent;
            panel1.Visible = true;
            this.BackColor = Color.White;
            this.panel1.BackColor = Color.Transparent;


            
            listele();
            listele1();
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderText = "Müşteri Kodu";
            dataGridView1.Columns[2].HeaderText = "Ad";
            dataGridView1.Columns[3].HeaderText = "Soyad";
            dataGridView1.Columns[4].HeaderText = "Firma";
            dataGridView1.Columns[5].HeaderText = "Telefon";
            dataGridView1.Columns[6].HeaderText = "Adres";
            dataGridView1.Columns[7].HeaderText = "Bakiye";

            con = new SQLiteConnection("Data Source=yemektakip.sqlite;Version=3;");
            da = new SQLiteDataAdapter("Select *From ayar", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "ayar");
            dataGridView2.DataSource = ds.Tables["ayar"];
            con.Close();
            linkLabel3.Text = dataGridView2.CurrentRow.Cells[1].Value.ToString();
            linkLabel4.Text = "0";

            

            timer1.Start();
            listele();
            textBox1.Select();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            listele1();
            
            string isimgiris = Microsoft.VisualBasic.Interaction.InputBox("Şifre Girin!", "İşleme devam edebilmek için yönetici şifrenizi giriniz.", "", 0, 0);

            if (isimgiris == linkLabel3.Text)
            {
                frm2.ShowDialog();
            }
            else
            {
                MessageBox.Show("Yanlış şifre");
            }
            textBox1.Select();
            

           
        }


        
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int textuz = textBox1.Text.Length;
            if (textuz >=7)
            {
                
                Console.Beep(2000, 1000);
                textBox1.Clear();
                textBox1.Select();
                textBox1.Clear();
                dataGridView1.DataSource = ds.Tables["musteriler"];

            }
            if (textuz == 6)
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


                try
                {
                    try
                    {

                        subak = Convert.ToInt32(textBox7.Text);
                        dusbak = Convert.ToInt32(textBox8.Text);

                        kalanbak = subak - dusbak;
                        linkLabel2.Text = kalanbak.ToString();
                        if (kalanbak >= 0)
                        {
                            cmd = new SQLiteCommand();
                            con.Open();
                            cmd.Connection = con;
                            cmd.CommandText = "update musteriler set bakiye='" + linkLabel2.Text + "' where id=" + linkLabel1.Text + "";
                            cmd.ExecuteNonQuery();
                            con.Close();

                            try
                            {
                                string tarih;
                                DateTime dt = DateTime.Now;

                                tarih = DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss");

                                con.Open();
                                cmd.CommandText = "insert into rapor(tarih,ad,soyad,firma,dusen,eklenen,mkodu) values ('" + tarih + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox8.Text + "','" + "0" + "','" + linkLabel4.Text + "')";
                                cmd.ExecuteNonQuery();
                                con.Close();
                                Console.Beep(2000, 300);
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
                            textBox8.Text = "1";
                        }
                        else
                        {
                            Console.Beep(2000, 1000);
                            MessageBox.Show("Yetersiz Bakiye");
                            
                        }
                    }
                    catch (Exception hata)
                    {
                        Console.Beep(2000, 300);
                        hata.ToString();
                        
                    }
                    
                }
                catch (Exception hata)
                {
                    Console.Beep(2000, 1000);
                    MessageBox.Show(hata.ToString());
                    
                }
            }


            textBox1.Select();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            label1.Text = "Tarih :"+ DateTime.Now.ToLongDateString() + "\r\n" + "Saat :" + DateTime.Now.ToLongTimeString();
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            linkLabel4.Text = "0";
            linkLabel1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            linkLabel4.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox2.Text= dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox3.Text= dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox4.Text= dataGridView1.CurrentRow.Cells[4].Value.ToString();
            textBox5.Text= dataGridView1.CurrentRow.Cells[5].Value.ToString();
            textBox6.Text= dataGridView1.CurrentRow.Cells[6].Value.ToString();
            textBox7.Text= dataGridView1.CurrentRow.Cells[7].Value.ToString();
            linkLabel4.Text = "0";

        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Program kapatılsın mı ?", "Çıkış", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                this.Close();
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listele1();

            string isimgiris = Microsoft.VisualBasic.Interaction.InputBox("Şifre Girin!", "İşleme devam edebilmek için yönetici şifrenizi giriniz.", "", 0, 0);

            if (isimgiris == linkLabel3.Text)
            {
                frm3.ShowDialog();
            }
            else
            {
                MessageBox.Show("Yanlış şifre");
            }
            textBox1.Select();
        
        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Select();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            frm4.ShowDialog();
            textBox1.Select();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            listele1();

            string isimgiris = Microsoft.VisualBasic.Interaction.InputBox("Şifre Girin!", "İşleme devam edebilmek için yönetici şifrenizi giriniz.", "", 0, 0);

            if (isimgiris == linkLabel3.Text)
            {
                frm5.ShowDialog();
            }
            else
            {
                MessageBox.Show("Yanlış şifre");
            }
            textBox1.Select();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            listele1();

            string isimgiris = Microsoft.VisualBasic.Interaction.InputBox("Şifre Girin!", "İşleme devam edebilmek için yönetici şifrenizi giriniz.", "", 0, 0);

            if (isimgiris == linkLabel3.Text)
            {
                frm6.ShowDialog();
            }
            else
            {
                MessageBox.Show("Yanlış şifre");
            }
            textBox1.Select();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {

                subak = Convert.ToInt32(textBox7.Text);
                dusbak = Convert.ToInt32(textBox8.Text);

                kalanbak = subak - dusbak;
                linkLabel2.Text = kalanbak.ToString();
                if (kalanbak >= 0)
                {
                    cmd = new SQLiteCommand();
                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = "update musteriler set bakiye='" + linkLabel2.Text + "' where id=" + linkLabel1.Text + "";
                    cmd.ExecuteNonQuery();
                    con.Close();

                    try
                    {
                        string tarih;
                        DateTime dt = DateTime.Now;

                        tarih = DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss");

                        con.Open();
                        cmd.CommandText = "insert into rapor(tarih,ad,soyad,firma,dusen,eklenen,mkodu) values ('" + tarih + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox8.Text + "','" + "0" + "','" + linkLabel4.Text + "')";
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
                    textBox8.Text = "1";
                                        }
                else
                {
                    MessageBox.Show("Yetersiz Bakiye");
                }
            }
            catch ( Exception hata)
            {

                hata.ToString();
            }
            textBox1.Select();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (radioButton1.Checked == false)
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }

            else
            {
                e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)&& !char.IsSeparator(e.KeyChar);
            }
        }
    }
}
    
