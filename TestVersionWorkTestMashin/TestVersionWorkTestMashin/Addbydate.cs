using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestVersionWorkTestMashin.Controller;

namespace TestVersionWorkTestMashin
{
    
    public partial class Addbydate : Form
    {
        Query qrnew;
        Query qr;
        Query thqr;
        public Addbydate()
        {
            InitializeComponent();
            qrnew = new Query(ConnectionString.connstr);
            qr = new Query(ConnectionString.connstr);
            thqr = new Query(ConnectionString.connstr);
        }

        private void button1_Click(object sender, EventArgs e)
        {            
            if (textBox1.Text == "") {
                textBox1.Text= "" + DateTime.Now.ToString("dd.MM.yyyy") + "";
                textBox1.ForeColor = Color.Gray;
            }
            qr.CreateDateT(textBox1.Text);
            dataGridView1.DataSource = qrnew.EditUpdateTable("SELECT * FROM DateT Order by ID Desc");
          


        }

        private void Addbydate_Load(object sender, EventArgs e)
        {
           dataGridView1.DataSource = qrnew.EditUpdateTable("SELECT * FROM DateT Order by Id Desc");
           dataGridView2.DataSource = thqr.EditUpdateTable("SELECT t.* FROM  Thing as t WHERE t.id<>(select Thing from TimeThink)");





           dataGridView1.RowHeadersVisible = false;
           dataGridView1.Columns[0].Visible = false;
           dataGridView1.Columns[1].HeaderText = "Дата исполнения";
           dataGridView2.RowHeadersVisible = false;
           dataGridView2.Columns[0].Visible = false;
           dataGridView2.Columns[5].Visible = false;
            dataGridView2.Columns[1].HeaderText = "Наименование";
            dataGridView2.Columns[2].HeaderText = "Материал";
            dataGridView2.Columns[3].HeaderText = "Метод чистки";
            dataGridView2.Columns[4].HeaderText = "Билет на чистку";
            textBox1.Text = ""+DateTime.Now.ToString("dd.MM.yyyy")+"";
            textBox1.ForeColor = Color.Gray;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            qrnew.DeleteDateT(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["ID"].Value.ToString());
            dataGridView1.DataSource = qrnew.EditUpdateTable("SELECT * FROM DateT Order by ID Desc");
        }

        private void textBox1_CursorChanged(object sender, EventArgs e)
        {
            textBox1.ForeColor = Color.Black;
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.ForeColor = Color.Black;
            textBox1.Text = "";
        }

        private void textBox1_AcceptsTabChanged(object sender, EventArgs e)
        {
            textBox1.ForeColor = Color.Black;
            textBox1.Text = "";
        }

        private void textBox1_TabIndexChanged(object sender, EventArgs e)
        {
            textBox1.ForeColor = Color.Black;
            textBox1.Text = "";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.ForeColor = Color.Black;
        }
    }
}
