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

namespace Yemek_Takip
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        SQLiteConnection con;
        SQLiteDataAdapter da;
        DataSet ds;


        public void topcik()
        {
            Decimal toplam1 = 0;
            for (int k = 0; k < dataGridView1.Rows.Count; ++k)
            {
                toplam1 += Convert.ToDecimal(dataGridView1.Rows[k].Cells[5].Value);
                label4.Text = toplam1.ToString();
            }
        }

        public void topek()
        {
            Decimal toplam2 = 0;
            for (int k = 0; k < dataGridView1.Rows.Count; ++k)
            {
                toplam2 += Convert.ToDecimal(dataGridView1.Rows[k].Cells[6].Value);
                label5.Text = toplam2.ToString();
            }
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            listele();
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderText = "Tarih";
            dataGridView1.Columns[2].HeaderText = "Ad";
            dataGridView1.Columns[3].HeaderText = "Soyad";
            dataGridView1.Columns[4].HeaderText = "Firma";
            dataGridView1.Columns[5].HeaderText = "Çıkan";
            dataGridView1.Columns[6].HeaderText = "Eklenen";

            timer1.Start();
            listele();
            topcik();
            topek();

        }

        public void listele()
        {

            con = new SQLiteConnection("Data Source=yemektakip.sqlite;Version=3;");
            da = new SQLiteDataAdapter("Select *From rapor", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "rapor");
            dataGridView1.DataSource = ds.Tables["rapor"];
            con.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = "Tarih :" + DateTime.Now.ToLongDateString() + "\r\n" + "Saat :" + DateTime.Now.ToLongTimeString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string aranan = textBox1.Text.Trim().ToUpper();
            da = new SQLiteDataAdapter("SELECT id, tarih, ad, soyad, firma, dusen, eklenen FROM rapor Where tarih BETWEEN @tar1 and @tar2 and ad like '" + aranan + "%'", con);
            ds = new DataSet();
            da.SelectCommand.Parameters.AddWithValue("@tar1", dateTimePicker1.Value);
            da.SelectCommand.Parameters.AddWithValue("@tar2", dateTimePicker2.Value);
            con.Open();
            da.Fill(ds, "rapor");
            dataGridView1.DataSource = ds.Tables["rapor"];
            con.Close();
            topcik();
            topek();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
