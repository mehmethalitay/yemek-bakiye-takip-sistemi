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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        SQLiteConnection con;
        SQLiteDataAdapter da;
        SQLiteCommand cmd;
        DataSet ds;

        public void listele()
        {
            con = new SQLiteConnection("Data Source=yemektakip.sqlite;Version=3;");
            da = new SQLiteDataAdapter("Select *From ayar", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "ayar");
            dataGridView1.DataSource = ds.Tables["ayar"];
            con.Close();
            linkLabel1.Text= dataGridView1.CurrentRow.Cells[0].Value.ToString();
            linkLabel2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void button3_Click(object sender, EventArgs e)
        {
             if (textBox1.Text == linkLabel2.Text && textBox2.Text == textBox3.Text)
             {
                cmd = new SQLiteCommand();
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "update ayar set sifre ='" + textBox2.Text + "' where id=" + linkLabel1.Text + "";
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Şifre başarı ile değiştirildi. Programı yeniden başlatın !  ");
                listele();
                Application.Exit();
                 
             }
            else
            {
                MessageBox.Show("Şifre yanlış veya uyuşmuyor !");
                listele();
            }
                
            listele();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form6_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }
    }
}
